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
    public class TagsController : ControllerBase
    {
        private readonly BookanizerDbContext _context;
        private readonly IMapper _mapper;

        public TagsController(BookanizerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(TagDTO tagDTO)
        {
            _context.Tag.Add(_mapper.Map<Tag>(tagDTO));

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(TagDTO tagDTO)
        {
            var dbTag = await _context.Tag.FindAsync(tagDTO.Id);

            if (dbTag == null)
            {
                return NotFound();
            }

            _mapper.From(tagDTO).AdaptTo(dbTag);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var tag = await _context.Tag.FindAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            _context.Tag.Remove(tag);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("updatetags")]
        public async Task<IActionResult> UpdateTags(UpdateTagDTO updateTagDTO)
        {
            var book = _context.Books.Include(x=>x.BookTags).FirstOrDefault(x=>x.Id == updateTagDTO.BookId);
            if (book == null)
            {
                return NotFound();
            }
            if(book.BookTags.Any())
            {
                _context.BookTags.RemoveRange(book.BookTags);
            }

            foreach (var tagId in updateTagDTO.TagIds)
            {
                _context.BookTags.Add(new()
                {
                    BookId = updateTagDTO.BookId,
                    TagId = tagId
                });
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<List<TagDTO>> GetAll()
        {
            return _mapper.Map<List<TagDTO>>(await _context.Tag.ToListAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<TagDTO>> GetById(Guid id)
        {
            var tag = await _context.Tag.FindAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TagDTO>(tag));
        }

        [HttpGet("books/{id}")]
        public async Task<ActionResult<List<BookDTO>>> GetAllBooksForGenre(Guid id)
        {
            var bookTags = await _context.BookTags.Where(x => x.TagId == id).ToListAsync();

            if (!bookTags.Any())
            {
                return new List<BookDTO>();
            }

            List<BookDTO> bookDtos = new();
            
            foreach (var bookTag in bookTags)
            {
                bookDtos.Add((await _context.Books
                    .Include(x => x.Author)
                    //.Include(x=>x.Series)
                    .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                    .Include(tg => tg.BookTags)
                    .ThenInclude(t => t.Tag)
                    .FirstOrDefaultAsync(x => x.Id == bookTag.BookId)).Adapt<BookDTO>());
            }
            
            return Ok(bookDtos);
        }

        [HttpGet("name/{name}")]
        public async Task<List<TagDTO>> GetByName(string name)
        {
            var tags = await _context.Tag.Where(x => x.Name.Contains(name)).ToListAsync();

            if (!tags.Any())
            {
                return new();
            }

            return _mapper.Map<List<TagDTO>>(tags);
        }
    }
}
