using Bookanizer.Server.Interfaces;
using Bookanizer.Server.Model;
using Bookanizer.Server.Services;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Bookanizer.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly BookanizerDbContext _context;
        private readonly IMapper _mapper;

        public TestController(BookanizerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Test>>> GetTest()
        {
            var tests = _context.Test.ToList();
            return Ok(tests);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTest(string name)
        {
            Test test = new Test();
            test.FirstName = name;
            test.LastName  = name;
            test.YIppe = 2;
            _context.Test.Add(test);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
