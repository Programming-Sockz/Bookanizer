using Bookanizer.Server.Interfaces;
using Bookanizer.Server.Services;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Bookanizer.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly BookanizerDbContext context;
        private readonly IMapper mapper;

        public TestController(BookanizerDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetPersons()
        {
            return Ok("yippeee");
        }
    }
}
