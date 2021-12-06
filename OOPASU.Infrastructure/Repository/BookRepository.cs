using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OOPASU.Domain;
using OOPASU.Domain.DTO;

namespace OOPASU.Infrastructure.Repository
{
    public class BookRepository
    {
        private readonly Context _context;
        public BookRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }
        public async Task<Guid> AddAsync(BookDTO bookDTO)
        {
            var book = new Book()
            {
                Organisation = bookDTO.Organisation,
                Level = bookDTO.Level,
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book.Id;
        }
        public async Task<BookOutDTO> GetByIdAsync(Guid id)
        {
            Book book = await _context.Books.FindAsync(id);
            BookOutDTO dto = new BookOutDTO()
            {
                Id = book.Id,
                Organisation = book.Organisation,
                Level = book.Level,
            };
            return dto;
        }
        public async Task UpdateAsync(BookOutDTO book)
        {
            Book existBook = await _context.Books.FindAsync(book.Id);
            _context.Entry(existBook).CurrentValues.SetValues(book);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            Book book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return false;
            }
            else
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
