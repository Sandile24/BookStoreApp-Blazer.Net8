using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreAppAPI.Data;
using AutoMapper;
using BookStoreAppAPI.DTO_s.Book;
using AutoMapper.QueryableExtensions;

namespace BookStoreAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;

        public BooksController(BookStoreDbContext context, IMapper mapper, ILogger<BooksController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookReadDTO>>> GetBooks()
        {
            //using the try catch method to handle the error message for the userexc
            try
            {
                var books = await _context.Books
                    .Include(a=>a.Author)
                    .ProjectTo<BookReadDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                //var booksDTO = _mapper.Map<List<BookReadDTO>>(books);
                return Ok(books);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error Perfoming  GET in{nameof(GetBooks)}");
                return BadRequest("There ws an error, please try again");
            }
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailsDTO>> GetBook(int id)
        {
            var book = await _context.Books
                .Include(a => a.Author)
                .ProjectTo<BookDetailsDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a=>a.Id==id);

            if (book == null)
            {
                return NotFound($"There is no book the Id Number {id} in the database");
            }

            //var bookDTO = _mapper.Map<BookReadDTO>(book);

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDTO bookDTO)
        {
            var book = _context.Books.ToListAsync();
            if (id != bookDTO.Id)
            {
                return BadRequest();
            }

            if (book == null)
                return BadRequest($"Book with Id number {id} is not available");
             await _mapper.Map(bookDTO, book);
            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookCreateDTO>> PostBook(BookCreateDTO bookDTO)
        {
            
            var book = _mapper.Map<Book>(bookDTO);
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
