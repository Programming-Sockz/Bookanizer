using Bookanizer.Server.Model;
using Bookanizer.Server.Services;
using Bookanizer.Shared.DTO;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookanizer.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly BookanizerDbContext _context;
        private readonly IMapper _mapper;

        public GenresController(BookanizerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(GenreDTO genreDTO)
        {
            _context.Genre.Add(_mapper.Map<Genre>(genreDTO));

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(GenreDTO genreDTO)
        {
            var dbGenre = await _context.Genre.FindAsync(genreDTO.Id);

            if (dbGenre == null)
            {
                return NotFound();
            }

            _mapper.From(genreDTO).AdaptTo(dbGenre);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var genre = await _context.Genre.FindAsync(id);

            if(genre == null)
            {
                return NotFound();
            }

            _context.Genre.Remove(genre);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("updategenres")]
        public async Task<IActionResult> UpdateGenres(UpdateGenreDTO updateGenreDTO)
        {
            var book = _context.Books
                           .Include(b => b.BookGenres)
                           .FirstOrDefault(b => b.Id == updateGenreDTO.BookId);
            if (book == null)
            {
                return NotFound();
            }

            _context.BookGenres.RemoveRange(book.BookGenres);

            foreach (var genreId in updateGenreDTO.GenreIds)
            {
                _context.BookGenres.Add(new()
                {
                    BookId = updateGenreDTO.BookId,
                    GenreId = genreId
                });
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<List<GenreDTO>> GetAll()
        {
            return _mapper.Map<List<GenreDTO>>(await _context.Genre.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDTO>> GetById(Guid id)
        {
            var genre = await _context.Genre.FindAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GenreDTO>(genre));
        }

        [HttpGet("books/{id}")]
        public async Task<ActionResult<List<BookDTO>>> GetAllBooksForGenre(Guid id)
        {
            var bookGenres = await _context.BookGenres.Where(x => x.GenreId == id).ToListAsync();

            if (!bookGenres.Any())
            {
                return new List<BookDTO>();
            }

            List<BookDTO> bookDtos = new();
            
            foreach (var bookGenre in bookGenres)
            {
                bookDtos.Add((await _context.Books
                    .Include(x => x.Author)
                    //.Include(x=>x.Series)
                    .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                    .Include(tg => tg.BookTags)
                    .ThenInclude(t => t.Tag)
                    .FirstOrDefaultAsync(x => x.Id == bookGenre.BookId)).Adapt<BookDTO>());
            }
            
            return Ok(bookDtos);
        }

        [HttpGet("name/{name}")]
        public async Task<List<GenreDTO>> GetByName(string name)
        {
            var genres = await _context.Genre.Where(x => x.Name.Contains(name)).ToListAsync();

            if (!genres.Any())
            {
                return new();
            }

            return _mapper.Map<List<GenreDTO>>(genres);
        }
    }
}
