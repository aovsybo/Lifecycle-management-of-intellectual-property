using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOPASU.Domain;
using OOPASU.Domain.DTO;
using OOPASU.Infrastructure.Data;
using OOPASU.Infrastructure.Repository;

namespace OOPASU.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly Context _context;
        private readonly AuthorRepository _authorRepository;
        public AuthorsController(Context context)
        {
            _context = context;
            _authorRepository = new AuthorRepository(_context);
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            //return await _context.Authors.ToListAsync();
            return await _authorRepository.GetAllAsync();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorOutDTO>> GetAuthor(Guid id)
        {
            //var author = await _context.Authors.FindAsync(id);
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            AuthorOutDTO dto = new AuthorOutDTO()
            {
                Id = author.Id,
                UserId = author.UserId,
                FirstName = author.FirstName,
                SecondName = author.SecondName
            };

            return dto;
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(Guid id, AuthorOutDTO author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            await _authorRepository.UpdateAsync(author);

            return NoContent();
            /*_context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();*/
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuthorOutDTO>> PostAuthor(AuthorDTO authorDTO)
        {
            //_context.Authors.Add(author);
            //await _context.SaveChangesAsync();

            var author = new Author()
            {
                UserId = authorDTO.UserId,
                FirstName = authorDTO.FirstName,
                SecondName = authorDTO.SecondName

            };

            Guid id = await _authorRepository.AddAsync(author);
            return CreatedAtAction("GetAuthor", new { id = id }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            //var author = await _context.Authors.FindAsync(id);
            var author = await _authorRepository.GetByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            //_context.Authors.Remove(author);
            //await _context.SaveChangesAsync();
            await _authorRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool AuthorExists(Guid id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
