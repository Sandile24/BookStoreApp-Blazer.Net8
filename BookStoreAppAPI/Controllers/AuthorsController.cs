using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreAppAPI.Data;
using AutoMapper;
using BookStoreAppAPI.Models.Author;
using Microsoft.AspNetCore.Authorization;
//using BookStoreAppAPI.DTO_s.Author;
using AutoMapper.QueryableExtensions;

namespace BookStoreAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<ActionResult<AuthorDetailsDTO>> GetAuthor(int id)
        {
            var author = await _context.Authors.Include(q => q.Books)
                .ProjectTo<AuthorDetailsDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id == id);
            if (author == null)
            {
                return NotFound($"There is no Author with ID {id} on the database");
            }
            return author;
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{Id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutAuthor(int Id, AuthorUpdateDTO authorDTO)
        {
            if (Id != authorDTO.Id)
            {
                return BadRequest();
            }

            var author = await _context.Authors.FindAsync(Id);

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
                if (!await AuthorExists(Id))
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
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<AuthorCreateDTO>> PostAuthor(AuthorCreateDTO authorDTO)
        {
            var author = _mapper.Map<Author>(authorDTO);
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthors), new { id = author.Id }, author);            
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
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
