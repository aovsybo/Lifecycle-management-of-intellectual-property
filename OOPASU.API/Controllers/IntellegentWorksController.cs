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
    public class IntellegentWorksController : ControllerBase
    {
        private readonly Context _context;
        private readonly IntellegentWorkRepository _intellegentWorkRepository;
        public IntellegentWorksController(Context context)
        {
            _context = context;
            _intellegentWorkRepository = new IntellegentWorkRepository(_context);
        }

        // GET: api/IntellegentWorks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IntellegentWork>>> GetIntellegentWorks()
        {
            //return await _context.IntellegentWorks.ToListAsync();
            return await _intellegentWorkRepository.GetAllAsync();
        }

        // GET: api/IntellegentWorks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IntellegentWorkOutDTO>> GetIntellegentWork(Guid id)
        {
            //var intellegentWork = await _context.IntellegentWorks.FindAsync(id);
            var intellegentWork = await _intellegentWorkRepository.GetByIdAsync(id);
            if (intellegentWork == null)
            {
                return NotFound();
            }

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

        // PUT: api/IntellegentWorks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIntellegentWork(Guid id, IntellegentWorkOutDTO intellegentWork)
        {
            if (id != intellegentWork.Id)
            {
                return BadRequest();
            }

            await _intellegentWorkRepository.UpdateAsync(intellegentWork);

            return NoContent();
            /*_context.Entry(intellegentWork).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IntellegentWorkExists(id))
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

        // POST: api/IntellegentWorks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IntellegentWorkOutDTO>> PostIntellegentWork(IntellegentWorkDTO intellegentWorkDTO)
        {
            //_context.IntellegentWorks.Add(intellegentWork);
            //await _context.SaveChangesAsync();

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

            Guid id = await _intellegentWorkRepository.AddAsync(intellegentWork);
            return CreatedAtAction("GetIntellegentWork", new { id = id }, intellegentWork);
        }

        // DELETE: api/IntellegentWorks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIntellegentWork(Guid id)
        {
            //var intellegentWork = await _context.IntellegentWorks.FindAsync(id);
            var intellegentWork = await _intellegentWorkRepository.GetByIdAsync(id);
            if (intellegentWork == null)
            {
                return NotFound();
            }

            //_context.IntellegentWorks.Remove(intellegentWork);
            //await _context.SaveChangesAsync();
            await _intellegentWorkRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool IntellegentWorkExists(Guid id)
        {
            return _context.IntellegentWorks.Any(e => e.Id == id);
        }
    }
}