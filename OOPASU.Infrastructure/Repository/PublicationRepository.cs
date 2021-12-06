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
        public async Task<Guid> AddAsync(PublicationDTO publicationDTO)
        {
            var publication = new Publication()
            {
                Publisher = publicationDTO.Publisher,
                UDK = publicationDTO.UDK,
                Editor = publicationDTO.Editor,
                PageNumber = publicationDTO.PageNumber
            };
            _context.Publications.Add(publication);
            await _context.SaveChangesAsync();
            return publication.Id;
        }
        public async Task<PublicationOutDTO> GetByIdAsync(Guid id)
        {
            Publication publication = await _context.Publications.FindAsync(id);
            PublicationOutDTO dto = new PublicationOutDTO()
            {
                Id = publication.Id,
                Publisher = publication.Publisher,
                UDK = publication.UDK,
                Editor = publication.Editor,
                PageNumber = publication.PageNumber
            };
            return dto;
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
