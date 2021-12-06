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
        public async Task<Guid> AddAsync(AuthorDTO authorDTO)
        {
            var author = new Author()
            {
                UserId = authorDTO.UserId,
                FirstName = authorDTO.FirstName,
                SecondName = authorDTO.SecondName

            };
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author.Id;
        }
        public async Task<AuthorOutDTO> GetByIdAsync(Guid id)
        {
            Author author = await _context.Authors.FindAsync(id);
            AuthorOutDTO dto = new AuthorOutDTO()
            {
                Id = author.Id,
                UserId = author.UserId,
                FirstName = author.FirstName,
                SecondName = author.SecondName
            };
            return dto;
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
