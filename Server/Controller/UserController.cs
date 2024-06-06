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
    public class UserController : ControllerBase
    {
        private readonly BookanizerDbContext _context;
        private readonly IMapper _mapper;

        public UserController(BookanizerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<UserDTO>>> GetUsers()
        {
            //Mit diesem .Select() kann man nur bestimmte Sachen abgreifen
            //SQL equivalent ist einfach nicht die Ganze tabelle ausspucken =
            //SELECT Id, UserName, CreatedOn
            //FROM User;
            //und mit dem new UserDTO() kann ich aus diesen daten direkt unser UserDTO erstellen.
            var users = _context.User.Where(x=>x.Active).Select(x => new UserDTO(){ Id = x.Id, UserName = x.UserName, CreatedOn = x.CreatedOn}).ToList();
            
            
            //man könnte auch erst alle user holen und dann in das UserDTO umwandeln
            //var users = _context.User.ToList();
            //List<UserDTO> userDTOs = mapper.Map<List<UserDTO>>(users);
            //aber das ist halt langsamer
            //und ich weis actually nicht ob das geht bin zu faul gerade zu testen lol

            return Ok(users);
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(Guid id)
        {
            //wir benutzen das selbe select aber haben jetzt ein FirstOrDefault.
            //.FirstOrDefault() und .First() machen das selbe ab .First() wirft ein Fehler wenn nichts gefunden wurde und .FirstOrDefault() gibt null zurück
            // ist nützlich um errors leicht zu handeln
            var user = await _context.User.Where(x=>x.Active && x.Id == id).Select(x => new UserDTO() { Id = x.Id, UserName = x.UserName, CreatedOn = x.CreatedOn }).FirstOrDefaultAsync();

            if(user == null)
            {
                //error 404
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<LoginResponseDTO> RegisterUser(RegisterDTO registerDTO)
        {
            // check if email already exists
            if(_context.User.Any(x=>x.Email == registerDTO.Email))
            {
                return new LoginResponseDTO() { Success = false, ErrorMessage = "Email already exists" };
            }
            //check if Username already exists
            else if(_context.User.Any(x=>x.UserName == registerDTO.UserName))
            {
                return new LoginResponseDTO() { Success = false, ErrorMessage = "Username already exists" };
            }

            var user = new User()
            {
                //man könnte einmal noch die Id erstellen aber das ist bad practice. unser SQL erstellt die selber
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                Password = registerDTO.Password,
                CreatedOn = DateTime.Now,
                LastLogin = DateTime.Now,
                Active = true
            };

            _context.User.Add(user);

            await _context.SaveChangesAsync();

            //TODO: actually make a Trackersystem to see who is logged in with LoginStamp
            //Ich benutz hier user.Id weil die in diesen fall nach dem _context.SaveChangesAsync() erstellt wurde.
            return new LoginResponseDTO() { Success = true, LoginStamp = Guid.NewGuid(), UserId = user.Id };
        }

        [HttpPost("login")]
        public async Task<LoginResponseDTO> LoginUser(LoginDTO loginDTO)
        {
            //das ! hier bedeutet das nachfolgende true or fals ist reversed.
            if (!_context.User.Any(x=>x.Email == loginDTO.Email))
            {
                return new LoginResponseDTO() { Success = false, ErrorMessage = "Email doesn't exist" };
            }
            else
            {
                var user = await _context.User.FirstAsync(x => x.Email == loginDTO.Email);
                if(!user.Active)
                {
                    return new LoginResponseDTO() { Success = false, ErrorMessage = "User is deactivated" };
                }
                else if (user.Password == loginDTO.Password)
                {
                    return new LoginResponseDTO() { Success = true, LoginStamp = Guid.NewGuid(), UserId = user.Id };
                }
                else
                {
                    return new LoginResponseDTO() { Success = false, ErrorMessage = "Password is incorrect" };
                }
            }
        }




        //keine ahnung ob ich das benutzen werde aber ich schreibs einfach mal das es da ist.
        [HttpPut("deactivate/{id}")]
        public async Task<IActionResult> DeactivateUser(Guid id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Active = false;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("activate/{id}")]
        public async Task<IActionResult> ActivateUser(Guid id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Active = true;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
