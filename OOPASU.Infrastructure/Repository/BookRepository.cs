using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OOPASU.Domain;
using OOPASU.Domain.DTO;
using OOPASU.Infrastructure.Data;

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
        public async Task<Guid> AddAsync(Book book)
        {
            
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book.Id;
        }
        public async Task<Book> GetByIdAsync(Guid id)
        {
            Book book = await _context.Books.FindAsync(id);
            
            return book;
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
