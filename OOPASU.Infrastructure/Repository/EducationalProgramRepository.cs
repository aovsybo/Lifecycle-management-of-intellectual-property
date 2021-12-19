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
    public class EducationalProgramRepository
    {
        private readonly Context _context;
        public EducationalProgramRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<EducationalProgram>> GetAllAsync()
        {
            return await _context.EducationalPrograms.ToListAsync();
        }
        public async Task<Guid> AddAsync(EducationalProgram educationalProgram)
        {
            _context.EducationalPrograms.Add(educationalProgram);
            await _context.SaveChangesAsync();
            return educationalProgram.Id;
        }
        public async Task<EducationalProgram> GetByIdAsync(Guid id)
        {
            EducationalProgram educationalProgram = await _context.EducationalPrograms.FindAsync(id);
            
            return educationalProgram;
        }
        public async Task UpdateAsync(EducationalProgramOutDTO educationalProgram)
        {
            EducationalProgram existEducationalProgram = await _context.EducationalPrograms.FindAsync(educationalProgram.Id);
            _context.Entry(existEducationalProgram).CurrentValues.SetValues(educationalProgram);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            EducationalProgram educationalProgram = await _context.EducationalPrograms.FindAsync(id);
            if (educationalProgram == null)
            {
                return false;
            }
            else
            {
                _context.EducationalPrograms.Remove(educationalProgram);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
