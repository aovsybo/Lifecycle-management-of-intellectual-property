using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOPASU.Domain;
using OOPASU.Domain.DTO;
using OOPASU.Infrastructure.Data;
using OOPASU.Infrastructure.Repository;

namespace OOPASU.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationSertificatesController : ControllerBase
    {
        private readonly Context _context;
        private readonly RegistrationSertificateRepository _registrationSertificateRepository;
        public RegistrationSertificatesController(Context context)
        {
            _context = context;
            _registrationSertificateRepository = new RegistrationSertificateRepository(_context);
        }

        // GET: api/RegistrationSertificates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistrationSertificate>>> GetRegistrationSertificates()
        {
            //return await _context.RegistrationSertificates.ToListAsync();
            return await _registrationSertificateRepository.GetAllAsync();
        }

        // GET: api/RegistrationSertificates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegistrationSertificate>> GetRegistrationSertificate(Guid id)
        {
            //var registrationSertificate = await _context.RegistrationSertificates.FindAsync(id);
            var registrationSertificate = await _registrationSertificateRepository.GetByIdAsync(id);
            if (registrationSertificate == null)
            {
                return NotFound();
            }

            RegistrationSertificateOutDTO dto = new RegistrationSertificateOutDTO()
            {
                Id = registrationSertificate.Id,
                ProductType = registrationSertificate.ProductType,
                ProductName = registrationSertificate.ProductName,
                Number = registrationSertificate.Number,
                RegistrationDate = registrationSertificate.RegistrationDate,
                RightHolder = registrationSertificate.RightHolder
            };

            return registrationSertificate;
        }

        // PUT: api/RegistrationSertificates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegistrationSertificate(Guid id, RegistrationSertificateOutDTO registrationSertificate)
        {
            if (id != registrationSertificate.Id)
            {
                return BadRequest();
            }

            await _registrationSertificateRepository.UpdateAsync(registrationSertificate);

            return NoContent();
            /*_context.Entry(registrationSertificate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrationSertificateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            */
            return NoContent();
        }

        // POST: api/RegistrationSertificates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RegistrationSertificateOutDTO>> PostRegistrationSertificate(RegistrationSertificateDTO registrationSertificateDTO)
        {
            //_context.RegistrationSertificates.Add(registrationSertificate);
            //await _context.SaveChangesAsync();

            var registrationSertificate = new RegistrationSertificate()
            {
                ProductType = registrationSertificateDTO.ProductType,
                ProductName = registrationSertificateDTO.ProductName,
                Number = registrationSertificateDTO.Number,
                RegistrationDate = registrationSertificateDTO.RegistrationDate,
                RightHolder = registrationSertificateDTO.RightHolder
            };

            Guid id = await _registrationSertificateRepository.AddAsync(registrationSertificate);
            return CreatedAtAction("GetRegistrationSertificate", new { id = id }, registrationSertificate);
        }

        // DELETE: api/RegistrationSertificates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistrationSertificate(Guid id)
        {
            //var registrationSertificate = await _context.RegistrationSertificates.FindAsync(id);
            var registrationSertificate = await _registrationSertificateRepository.GetByIdAsync(id);
            if (registrationSertificate == null)
            {
                return NotFound();
            }

            //_context.RegistrationSertificates.Remove(registrationSertificate);
            //await _context.SaveChangesAsync();
            await _registrationSertificateRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool RegistrationSertificateExists(Guid id)
        {
            return _context.RegistrationSertificates.Any(e => e.Id == id);
        }
    }
}
