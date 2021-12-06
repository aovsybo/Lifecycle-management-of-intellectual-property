using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOPASU.Domain;
using OOPASU.Domain.DTO;
using OOPASU.Infrastructure;
using OOPASU.Infrastructure.Repository;

namespace OOPASU.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationsController : ControllerBase
    {
        private readonly Context _context;
        private readonly PublicationRepository _publicationRepository;
        public PublicationsController(Context context)
        {
            _context = context;
            _publicationRepository = new PublicationRepository(_context);
        }

        // GET: api/Publications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publication>>> GetPublications()
        {
            //return await _context.Publications.ToListAsync();
            return await _publicationRepository.GetAllAsync();
        }

        // GET: api/Publications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicationOutDTO>> GetPublication(Guid id)
        {
            //var publication = await _context.Publications.FindAsync(id);
            var publication = await _publicationRepository.GetByIdAsync(id);
            if (publication == null)
            {
                return NotFound();
            }

            return publication;
        }

        // PUT: api/Publications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublication(Guid id, PublicationOutDTO publication)
        {
            if (id != publication.Id)
            {
                return BadRequest();
            }

            await _publicationRepository.UpdateAsync(publication);

            return NoContent();
            /*_context.Entry(publication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublicationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();*/
        }

        // POST: api/Publications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PublicationOutDTO>> PostPublication(PublicationDTO publication)
        {
            //_context.Publications.Add(publication);
            //await _context.SaveChangesAsync();
            Guid id = await _publicationRepository.AddAsync(publication);
            return CreatedAtAction("GetPublication", new { id = id }, publication);
        }

        // DELETE: api/Publications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublication(Guid id)
        {
            //var publication = await _context.Publications.FindAsync(id);
            var publication = await _publicationRepository.GetByIdAsync(id);
            if (publication == null)
            {
                return NotFound();
            }

            //_context.Publications.Remove(publication);
            //await _context.SaveChangesAsync();
            await _publicationRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool PublicationExists(Guid id)
        {
            return _context.Publications.Any(e => e.Id == id);
        }
    }
}
