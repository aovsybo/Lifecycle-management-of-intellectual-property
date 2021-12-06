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
        public async Task<Guid> AddAsync(IntellegentWorkDTO intellegentWorkDTO)
        {
            var intellegentWork = new IntellegentWork()
            {
 
                Title = intellegentWorkDTO.Title,
                Category = intellegentWorkDTO.Category,
                Description = intellegentWorkDTO.Description,
                GRNTI = intellegentWorkDTO.GRNTI,
                DOI = intellegentWorkDTO.DOI,
                Place = intellegentWorkDTO.Place,
                Year = intellegentWorkDTO.Year,
                Status = intellegentWorkDTO.Status
            };
            _context.IntellegentWorks.Add(intellegentWork);
            await _context.SaveChangesAsync();
            return intellegentWork.Id;
        }
        public async Task<IntellegentWorkOutDTO> GetByIdAsync(Guid id)
        {
            IntellegentWork intellegentWork = await _context.IntellegentWorks.FindAsync(id);
            IntellegentWorkOutDTO dto = new IntellegentWorkOutDTO()
            {
                Id = intellegentWork.Id,
                Title = intellegentWork.Title,
                Category = intellegentWork.Category,
                Description = intellegentWork.Description,
                GRNTI = intellegentWork.GRNTI,
                DOI = intellegentWork.DOI,
                Place = intellegentWork.Place,
                Year = intellegentWork.Year,
                Status = intellegentWork.Status
            };
            return dto;
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
