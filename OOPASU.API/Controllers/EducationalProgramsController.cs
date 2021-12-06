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
    public class EducationalProgramsController : ControllerBase
    {
        private readonly Context _context;
        private readonly EducationalProgramRepository _educationalProgramRepository;
        public EducationalProgramsController(Context context)
        {
            _context = context;
            _educationalProgramRepository = new EducationalProgramRepository(_context);
        }

        // GET: api/EducationalPrograms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EducationalProgram>>> GetEducationalPrograms()
        {
            //return await _context.EducationalPrograms.ToListAsync();
            return await _educationalProgramRepository.GetAllAsync();
        }

        // GET: api/EducationalPrograms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EducationalProgramOutDTO>> GetEducationalProgram(Guid id)
        {
            //var educationalProgram = await _context.EducationalPrograms.FindAsync(id);
            var educationalProgram = await _educationalProgramRepository.GetByIdAsync(id);
            if (educationalProgram == null)
            {
                return NotFound();
            }

            return educationalProgram;
        }

        // PUT: api/EducationalPrograms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEducationalProgram(Guid id, EducationalProgramOutDTO educationalProgram)
        {
            if (id != educationalProgram.Id)
            {
                return BadRequest();
            }

            await _educationalProgramRepository.UpdateAsync(educationalProgram);

            return NoContent();
            /*_context.Entry(educationalProgram).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EducationalProgramExists(id))
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

        // POST: api/EducationalPrograms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EducationalProgramOutDTO>> PostEducationalProgram(EducationalProgramDTO educationalProgram)
        {
            //_context.EducationalPrograms.Add(educationalProgram);
            //await _context.SaveChangesAsync();
            Guid id = await _educationalProgramRepository.AddAsync(educationalProgram);
            return CreatedAtAction("GetEducationalProgram", new { id = id }, educationalProgram);
        }

        // DELETE: api/EducationalPrograms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducationalProgram(Guid id)
        {
            //var educationalProgram = await _context.EducationalPrograms.FindAsync(id);
            var educationalProgram = await _educationalProgramRepository.GetByIdAsync(id);
            
            if (educationalProgram == null)
            {
                return NotFound();
            }

            //_context.EducationalPrograms.Remove(educationalProgram);
            //await _context.SaveChangesAsync();
            await _educationalProgramRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool EducationalProgramExists(Guid id)
        {
            return _context.EducationalPrograms.Any(e => e.Id == id);
        }
    }
}
