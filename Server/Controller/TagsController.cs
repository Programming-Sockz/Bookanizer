using Bookanizer.Server.Model;
using Bookanizer.Server.Services;
using Bookanizer.Shared.DTO;
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
    }
}
