using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreAppAPI.Data;
using AutoMapper;
using BookStoreAppAPI.DTO_s.Book;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksController(BookStoreDbContext context, IMapper mapper, ILogger<BooksController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;// For handling errors on users screen
            _webHostEnvironment = webHostEnvironment;
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
                return BadRequest("There was an error, please try again");
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDTO bookDTO)
        {
            var book = _context.Books.ToListAsync();
            if (id != bookDTO.Id)
            {
                return BadRequest();
            }
            if (book == null)
                return BadRequest($"Book with Id number {id} is not available");
            //To save the image
            if (string.IsNullOrEmpty(bookDTO.ImageData) == false)
            {
                bookDTO.Image = CreateFile(bookDTO.ImageData, bookDTO.OriginalImageName);
                var picName = Path.GetFileName(bookDTO.Image);
                var path = $"{_webHostEnvironment.WebRootPath}\\BookImages\\{picName}";
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

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
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<BookCreateDTO>> PostBook(BookCreateDTO bookDTO)
        {            
            var book = _mapper.Map<Book>(bookDTO);
            book.Image= CreateFile(bookDTO.ImageData, bookDTO.OriginalImageName);//here we call the image file method
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        [Authorize(Roles ="Administrator")]
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

        private string CreateFile(string imageBase64, string imageNamme) //Add the IwebHostEnvironment
        {
            var url = HttpContext.Request.Host.Value;
            var ext = Path.GetExtension(imageNamme);
            var filename = $"{Guid.NewGuid()}{ext}"; 

            var path = $"{_webHostEnvironment.WebRootPath}\\BookImages\\{filename}";
            var bytes = Convert.FromBase64String(imageBase64);

            var fileStream = System.IO.File.Create(path);
            fileStream.Write(bytes, 0, bytes.Length);
            fileStream.Close();

            return $"https://{url}/BookImages/{filename}";
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
