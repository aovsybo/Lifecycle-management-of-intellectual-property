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
    public class IntellegentWorkRepository
    {
        private readonly Context _context;
        public IntellegentWorkRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<IntellegentWork>> GetAllAsync()
        {
            return await _context.IntellegentWorks.ToListAsync();
        }
        public async Task<Guid> AddAsync(IntellegentWork intellegentWork)
        {
            _context.IntellegentWorks.Add(intellegentWork);
            await _context.SaveChangesAsync();
            return intellegentWork.Id;
        }
        public async Task<IntellegentWork> GetByIdAsync(Guid id)
        {
            /*IntellegentWork intellegentWork = await _context.IntellegentWorks.Where(w => w.Id == id)
                .Include(k => k.KeyWords)
                .FirstOrDefaultAsync();*/
            IntellegentWork intellegentWork = await _context.IntellegentWorks.FindAsync(id);

            return intellegentWork;
        }
        public async Task UpdateAsync(IntellegentWorkOutDTO intellegentWork)
        {
            IntellegentWork existIntellegentWork = await _context.IntellegentWorks.FindAsync(intellegentWork.Id);
            _context.Entry(existIntellegentWork).CurrentValues.SetValues(intellegentWork);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            IntellegentWork intellegentWork = await _context.IntellegentWorks.FindAsync(id);
            if (intellegentWork == null)
            {
                return false;
            }
            else
            {
                _context.IntellegentWorks.Remove(intellegentWork);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
