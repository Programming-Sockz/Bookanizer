using Bookanizer.Server.Interfaces;
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
    public class AuthorController : ControllerBase
    {
        private readonly BookanizerDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(BookanizerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AuthorDTO authorDTO)
        {
            _context.Author.Add(_mapper.Map<Author>(authorDTO));

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(AuthorDTO authorDTO)
        {
            var dbAuthor = await _context.Author.FindAsync(authorDTO.Id);

            if(dbAuthor == null)
            {
                return NotFound();
            }

            _mapper.From(authorDTO).AdaptTo(dbAuthor);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<List<AuthorDTO>> GetAllAuthors()
        {
            return _mapper.Map<List<AuthorDTO>>(await _context.Author.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthorWithBooks(Guid id)
        {
            var author = _context.Author.Find(id);

            if(author == null)
            {
                return NotFound();
            }

            var authorDTO = _mapper.Map<AuthorDTO>(author);
            authorDTO.Books = (await _context.Books
                .Include(x => x.Author)
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .Include(tg => tg.BookTags)
                .ThenInclude(t => t.Tag)
                .Where(x => x.AuthorId == id).ToListAsync()).Adapt<List<BookDTO>>();

            return authorDTO;
        }
    }
}
