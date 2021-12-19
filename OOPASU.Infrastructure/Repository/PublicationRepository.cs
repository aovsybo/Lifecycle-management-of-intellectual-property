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
    public class PublicationRepository
    {
        private readonly Context _context;
        public PublicationRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<Publication>> GetAllAsync()
        {
            return await _context.Publications.ToListAsync();
        }
        public async Task<Guid> AddAsync(Publication publication)
        {
            
            _context.Publications.Add(publication);
            await _context.SaveChangesAsync();
            return publication.Id;
        }
        public async Task<Publication> GetByIdAsync(Guid id)
        {
            Publication publication = await _context.Publications.FindAsync(id);
            
            return publication;
        }
        public async Task UpdateAsync(PublicationOutDTO publication)
        {
            Publication existPublication = await _context.Publications.FindAsync(publication.Id);
            _context.Entry(existPublication).CurrentValues.SetValues(publication);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            Publication publication = await _context.Publications.FindAsync(id);
            if (publication == null)
            {
                return false;
            }
            else
            {
                _context.Publications.Remove(publication);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
