using Bookanizer.Server.Interfaces;
using Bookanizer.Server.Model;
using Bookanizer.Server.Services;
using Bookanizer.Shared.DTO;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

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
    }
}
