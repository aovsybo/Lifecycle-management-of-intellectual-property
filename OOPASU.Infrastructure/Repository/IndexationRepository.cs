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
    public class IndexationRepository
    {
        private readonly Context _context;
        public IndexationRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<Indexation>> GetAllAsync()
        {
            return await _context.Indexations.ToListAsync();
        }
        public async Task<Guid> AddAsync(Indexation indexation)
        {
            
            _context.Indexations.Add(indexation);
            await _context.SaveChangesAsync();
            return indexation.Id;
        }
        public async Task<Indexation> GetByIdAsync(Guid id)
        {
            Indexation indexation = await _context.Indexations.FindAsync(id);
            
            return indexation;
        }
        public async Task UpdateAsync(IndexationOutDTO indexation)
        {
            Indexation existIndexation = await _context.Indexations.FindAsync(indexation.Id);
            _context.Entry(existIndexation).CurrentValues.SetValues(indexation);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            Indexation indexation = await _context.Indexations.FindAsync(id);
            if (indexation == null)
            {
                return false;
            }
            else
            {
                _context.Indexations.Remove(indexation);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
