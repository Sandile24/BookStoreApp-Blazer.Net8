using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreAppAPI.Data;
using BookStoreAppAPI.Models.Author;
using AutoMapper;

namespace BookStoreAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(BookStoreDbContext context,IMapper imapper, ILogger<AuthorsController> logger)
        {
            _context = context;
            _mapper = imapper;
            _logger = logger;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<List<AuthorReadDTO>>> GetAuthors()
        {
            //using the try catch method to handle the error message for the userexc
            try {
                var authors = await _context.Authors.ToListAsync();
                var authorDTO = _mapper.Map<List<AuthorReadDTO>>(authors);
                return Ok(authorDTO);
            }
            catch (Exception e)
            {
                _logger.LogError(e,$"Error Perfoming  GET in{ nameof(GetAuthors)}");
                return BadRequest("There ws an error, please try again");
            }
        }
        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReadDTO>> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound($"There is no Author with ID {id} on the database");
            }
            var authorDTO = _mapper.Map<AuthorReadDTO>(author);
            return authorDTO;
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDTO authorDTO)
        {
            if (id != authorDTO.Id)
            {
                return BadRequest();
            }
            var author = await _context.Authors.FindAsync();

            if (author == null)
                return NotFound();
            _mapper.Map(authorDTO, author);


            _context.Entry(author).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AuthorExists(id))
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

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuthorCreateDTO>> PostAuthor(AuthorCreateDTO authorDTO)
        {
            var author = _mapper.Map<Author>(authorDTO);
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthors), new { id = author.Id }, author);            
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private async Task<bool> AuthorExists(int id)
        {
             return await _context.Authors.AnyAsync(e => e.Id == id);
        }
    }
}
