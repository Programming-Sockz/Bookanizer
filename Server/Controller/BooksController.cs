using Bookanizer.Server.Model;
using Bookanizer.Server.Services;
using Bookanizer.Shared.DTO;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using static Bookanizer.Shared.Libraries.ApiRoutes;

namespace Bookanizer.Server.Controller
{
    //Diese klammern hier erstellen die Route für unsere API und sagt natürlich auch das dies ein APIController ist
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        //unsere SQL verbindung
        private readonly BookanizerDbContext _context;
        //mapper ist ein tool was hilft models in DTOs umzuwandeln weil wir die models nur im Server nutzen
        private readonly IMapper _mapper;

        //initialisiert den mapper und context
        public BooksController(BookanizerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //gibt an das dieses eine Get methode ist
        [HttpGet]
        //ActionResult ist ein HttpCode (z.b. 200 Succes, 404 Not Found, 500 Internal Error)
        public async Task<List<BookDTO>> Get()
        {
            //dieses var ist einfach nur das sich das programm selber raussuchen soll was das für ein typ ist.
            var books = _context.Books.ToList();

            //hier können wir sagen das nichts gefunden wurde und einen error 404 zurück geben
            if (books == null || books.Count == 0)
            {
                //machen wir aber jetzt mal nciht. wir erstellen einfach lokal ein test buch
                //return NotFound();

                books = [new Book() { Id = Guid.NewGuid(), PageCount = 100, ReleaseDate = DateTime.Now, Title = "Test Buch" }];
            }

            //nachdem das books hier jetzt aber ein model ist und nicht ein DTO wandeln wird das um
            var booksDTO = _mapper.Map<List<BookDTO>>(books);

            //wir geben die Liste zurück
            return booksDTO;
        }

        //in der Klammer kann man die erweiterte Route angeben. man kann auch ein Parameter der Funktion angeben 
        [HttpGet("name/{bookTitle}")]
        public async Task<ActionResult<List<BookDTO>>> GetByName(string bookTitle)
        {
            //Hier wird LinQ benutzt um nach den Namen zu sortieren
            //aka unsere Where abfrage
            var books = _context.Books.Where(x => x.Title == bookTitle).ToList();
            if (books == null)
            {
                return NotFound();
            }

            //man kann sich das mapping und return ein bischen abkürzen
            return Ok(_mapper.Map<List<BookDTO>>(books));
        }

        [HttpGet("id/{bookId}")]
        //nach einer Id zu suchen ist schneller und direkter als ein namen 
        public async Task<ActionResult<BookDTO>> GetById(Guid bookId)
        {
            var book = await _context.Books.FindAsync(bookId);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BookDTO>(book));
        }

        [HttpPost]
        //[FromBody] gibt an das dieser Parameter im Body des HttpRequest steht. wenn es nur ein Parameter gibt sucht es sich das automatisch raus aber ich schreibs hier nochmal rein um es klar zu machen
        public async Task<IActionResult> Post([FromBody]  BookDTO bookDTO)
        {
            if (bookDTO == null)
            {
                //error 500 irgendwas ist falsch beim tranfer geloffen. bracuht man aber nicht wirklich
                return BadRequest();
            }

            //fügt es dem Context hinzu
            _context.Add(_mapper.Map<Book>(bookDTO));

            //speichert diese ab
            await _context.SaveChangesAsync();

            //alles gut gelaufen. wir geben code 200 (succes) zurück
            return Ok();
        }

        [HttpPut("{bookId}")]
        //[FromHeader] gibt hier an das die bookId im Header steht
        public async Task<IActionResult> Put([FromHeader] Guid bookId, [FromBody] BookDTO bookDTO)
        {
            //wir brauchen zuerst unser momentan gespeichertes buch was wir überschreiben wollen
            var dbBook = await _context.Books.FindAsync(bookId);

            if(dbBook == null)
            {
                return NotFound();
            }

            //wir benutzen mapper einfach um es zu überspeichern
            _mapper.From(bookDTO).AdaptTo(dbBook);

            //wir speichern die Änderungen ab
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
