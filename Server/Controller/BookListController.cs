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
    public class BookListController : ControllerBase
    {
        private readonly BookanizerDbContext _context;
        private readonly IMapper _mapper;

        public BookListController(BookanizerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookList(BookListDTO bookListDTO)
        {
            var bookList = _mapper.Map<BookList>(bookListDTO);
            _context.BookList.Add(bookList);

            await _context.SaveChangesAsync();

            if (bookListDTO.Books.Any())
            {
                foreach (var book in bookListDTO.Books)
                {
                    _context.BookBookList.Add(new()
                    {
                        BookListId = bookList.Id,
                        BookId = book.Id
                    });
                }
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> PutBookList(BookListDTO bookListDTO)
        {
            var bookList = await _context.BookList.FindAsync(bookListDTO.Id);

            _mapper.From(bookList).AdaptTo(bookListDTO);
            await _context.SaveChangesAsync();

            var bookBookList = _context.BookBookList.Where(x => x.BookListId == bookListDTO.Id);

            if (bookBookList.Any())
            {
                _context.BookBookList.RemoveRange(bookBookList);
            }
            
            if (bookListDTO.Books.Any())
            {
                foreach (var book in bookListDTO.Books)
                {
                    _context.BookBookList.Add(new()
                    {
                        BookListId = bookList.Id,
                        BookId = book.Id
                    });
                }
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<List<BookListDTO>>> GetBookListForUser(Guid id)
        {
            // Fetch the user
            var user = await _context.User
                .Where(x => x.Active && x.Id == id)
                .Select(x => new UserDTO() { Id = x.Id, UserName = x.UserName, CreatedOn = x.CreatedOn })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound("User not found");
            }

            // Fetch the book lists
            var bookLists = await _context.BookList
                .Where(x => x.CreatedById == id)
                .ToListAsync();

            List<BookListDTO> bookListDTOs = new();

            foreach (var bookList in bookLists)
            {
                BookListDTO bookListDTO = _mapper.Map<BookListDTO>(bookList);
                bookListDTO.CreatedBy = user;

                // Fetch book IDs
                var bookIds = await _context.BookBookList
                    .AsNoTracking()
                    .Where(x => x.BookListId == bookList.Id)
                    .Select(x => x.BookId)
                    .ToListAsync();

                foreach (var bookId in bookIds)
                {
                    // Fetch the book including related entities
                    var book = await _context.Books
                        .Include(x => x.Author)
                        .Include(b => b.BookGenres)
                        .ThenInclude(bg => bg.Genre)
                        .Include(tg => tg.BookTags)
                        .ThenInclude(t => t.Tag)
                        .FirstOrDefaultAsync(x => x.Id == bookId);

                    if (book != null)
                    {
                        bookListDTO.Books.Add(book.Adapt<BookDTO>());
                    }
                }
                bookListDTOs.Add(bookListDTO);
            }

            return Ok(bookListDTOs);
        }

        [HttpGet("bookList/{id}")]
        public async Task<ActionResult<BookListDTO>> GetBookListById(Guid id)
        {
            var bookList = await _context.BookList.FindAsync(id);

            if (bookList == null)
            {
                return NotFound();
            }
            
            BookListDTO bookListDTO = new();

            bookListDTO = _mapper.Map<BookListDTO>(bookListDTO);
            
            var user = _context.User.Where(x=>x.Active && x.Id == id).Select(x => new UserDTO() { Id = x.Id, UserName = x.UserName, CreatedOn = x.CreatedOn }).FirstOrDefault();

            bookListDTO.CreatedBy = user;

            foreach (var bookId in _context.BookBookList.Where(x => x.BookListId == bookList.Id).Select(x => x.BookId))
            {
                bookListDTO.Books.Add(_context.Books
                    .Include(x => x.Author)
                    //.Include(x=>x.Series)
                    .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                    .Include(tg => tg.BookTags)
                    .ThenInclude(t => t.Tag)
                    .FirstOrDefault(x=>x.Id == bookId).Adapt<BookDTO>());
            }

            return Ok(bookListDTO);
        }

        [HttpPost("addbook")]
        public async Task<IActionResult> AddBookToList(BookBookListDTO bookBookListDTO)
        {
            if (_context.BookBookList.Any(x=>x.BookListId == bookBookListDTO.BookListId && x.BookId == bookBookListDTO.BookId))
            {
                return Ok("Book is already in List");
            }

            _context.BookBookList.Add(new()
            {
                BookListId = bookBookListDTO.BookListId,
                BookId = bookBookListDTO.BookId
            });
            await _context.SaveChangesAsync();

            return Ok("Book added");
        }
    }
}
