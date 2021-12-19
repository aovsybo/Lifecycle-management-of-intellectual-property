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
    public class AuthorRepository
    {
        private readonly Context _context;
        public AuthorRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }
        public async Task<Guid> AddAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author.Id;
        }
        public async Task<Author> GetByIdAsync(Guid id)
        {
            Author author = await _context.Authors.FindAsync(id);
            
            return author;
        }
        public async Task UpdateAsync(AuthorOutDTO author)
        {
            Author existAuthor = await _context.Authors.FindAsync(author.Id);
            _context.Entry(existAuthor).CurrentValues.SetValues(author);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            Author author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return false;
            }
            else
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
