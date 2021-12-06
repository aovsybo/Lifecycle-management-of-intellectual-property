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
    public class IndexationsController : ControllerBase
    {
        private readonly Context _context;
        private readonly IndexationRepository _indexationRepository;
        public IndexationsController(Context context)
        {
            _context = context;
            _indexationRepository = new IndexationRepository(_context);
        }

        // GET: api/Indexations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Indexation>>> GetIndexations()
        {
            //return await _context.Indexations.ToListAsync();
            return await _indexationRepository.GetAllAsync();
        }

        // GET: api/Indexations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IndexationOutDTO>> GetIndexation(Guid id)
        {
            //var indexation = await _context.Indexations.FindAsync(id);
            var indexation = await _indexationRepository.GetByIdAsync(id);
            if (indexation == null)
            {
                return NotFound();
            }

            return indexation;
        }

        // PUT: api/Indexations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIndexation(Guid id, IndexationOutDTO indexation)
        {
            if (id != indexation.Id)
            {
                return BadRequest();
            }

            await _indexationRepository.UpdateAsync(indexation);

            return NoContent();
            /*_context.Entry(indexation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IndexationExists(id))
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

        // POST: api/Indexations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IndexationOutDTO>> PostIndexation(IndexationDTO indexation)
        {
            //_context.Indexations.Add(indexation);
            //await _context.SaveChangesAsync();
            Guid id = await _indexationRepository.AddAsync(indexation);
            return CreatedAtAction("GetIndexation", new { id = id }, indexation);
        }

        // DELETE: api/Indexations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIndexation(Guid id)
        {
            //var indexation = await _context.Indexations.FindAsync(id);
            var indexation = await _indexationRepository.GetByIdAsync(id);
            if (indexation == null)
            {
                return NotFound();
            }

            //_context.Indexations.Remove(indexation);
            //await _context.SaveChangesAsync();
            await _indexationRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool IndexationExists(Guid id)
        {
            return _context.Indexations.Any(e => e.Id == id);
        }
    }
}
