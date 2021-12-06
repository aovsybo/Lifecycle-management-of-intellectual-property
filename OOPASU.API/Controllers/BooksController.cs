using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOPASU.Domain;
using OOPASU.Domain.DTO;
using OOPASU.Infrastructure;
using OOPASU.Infrastructure.Repository;

namespace OOPASU.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly Context _context;
        private readonly BookRepository _bookRepository;
        public BooksController(Context context)
        {
            _context = context;
            _bookRepository = new BookRepository(_context);
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            //return await _context.Books.ToListAsync();
            return await _bookRepository.GetAllAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookOutDTO>> GetBook(Guid id)
        {
            //var book = await _context.Books.FindAsync(id);
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(Guid id, BookOutDTO book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            await _bookRepository.UpdateAsync(book);

            return NoContent();
            /*_context.Entry(book).State = EntityState.Modified;

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

            return NoContent();*/
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookOutDTO>> PostBook(BookDTO book)
        {
            //_context.Books.Add(book);
            //await _context.SaveChangesAsync();
            Guid id = await _bookRepository.AddAsync(book);
            return CreatedAtAction("GetBook", new { id = id }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            //var book = await _context.Books.FindAsync(id);
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            //_context.Books.Remove(book);
            //await _context.SaveChangesAsync();
            await _bookRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool BookExists(Guid id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
