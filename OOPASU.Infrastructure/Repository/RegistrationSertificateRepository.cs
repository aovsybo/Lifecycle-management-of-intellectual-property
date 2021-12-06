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
    public class RegistrationSertificateRepository
    {
        private readonly Context _context;
        public RegistrationSertificateRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<RegistrationSertificate>> GetAllAsync()
        {
            return await _context.RegistrationSertificates.ToListAsync();
        }
        public async Task<Guid> AddAsync(RegistrationSertificateDTO registrationSertificateDTO)
        {
            var registrationSertificate = new RegistrationSertificate()
            {
                ProductType = registrationSertificateDTO.ProductType,
                ProductName = registrationSertificateDTO.ProductName,
                Number = registrationSertificateDTO.Number,
                RegistrationDate = registrationSertificateDTO.RegistrationDate,
                RightHolder = registrationSertificateDTO.RightHolder
            };
            _context.RegistrationSertificates.Add(registrationSertificate);
            await _context.SaveChangesAsync();
            return registrationSertificate.Id;
        }
        public async Task<RegistrationSertificateOutDTO> GetByIdAsync(Guid id)
        {
            RegistrationSertificate registrationSertificate = await _context.RegistrationSertificates.FindAsync(id);
            RegistrationSertificateOutDTO dto = new RegistrationSertificateOutDTO()
            {
                Id = registrationSertificate.Id,
                ProductType = registrationSertificate.ProductType,
                ProductName = registrationSertificate.ProductName,
                Number = registrationSertificate.Number,
                RegistrationDate = registrationSertificate.RegistrationDate,
                RightHolder = registrationSertificate.RightHolder 
            };
            return dto;
        }
        public async Task UpdateAsync(RegistrationSertificateOutDTO registrationSertificate)
        {
            RegistrationSertificate existRegistrationSertificate = await _context.RegistrationSertificates.FindAsync(registrationSertificate.Id);
            _context.Entry(existRegistrationSertificate).CurrentValues.SetValues(registrationSertificate);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            RegistrationSertificate registrationSertificate = await _context.RegistrationSertificates.FindAsync(id);
            if (registrationSertificate == null)
            {
                return false;
            }
            else
            {
                _context.RegistrationSertificates.Remove(registrationSertificate);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
