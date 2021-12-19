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
    public class KeyWordRepository
    {
        private readonly Context _context;
        public KeyWordRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<KeyWord>> GetAllAsync()
        {
            return await _context.KeyWords.ToListAsync();
        }
        public async Task<Guid> AddAsync(KeyWord keyWord)
        {
            
            _context.KeyWords.Add(keyWord);
            await _context.SaveChangesAsync();
            return keyWord.Id;
        }
        public async Task<KeyWord> GetByIdAsync(Guid id)
        {
            KeyWord keyWord = await _context.KeyWords.FindAsync(id);
            
            return keyWord;
        }
        public async Task UpdateAsync(KeyWordOutDTO keyWord)
        {
            KeyWord existKeyWord = await _context.KeyWords.FindAsync(keyWord.Id);
            _context.Entry(existKeyWord).CurrentValues.SetValues(keyWord);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            KeyWord keyWord = await _context.KeyWords.FindAsync(id);
            if (keyWord == null)
            {
                return false;
            }
            else
            {
                _context.KeyWords.Remove(keyWord);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
