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
    public class KeyWordsController : ControllerBase
    {
        private readonly Context _context;
        private readonly KeyWordRepository _keyWordRepository;
        public KeyWordsController(Context context)
        {
            _context = context;
            _keyWordRepository = new KeyWordRepository(_context);
        }

        // GET: api/KeyWords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KeyWord>>> GetKeyWords()
        {
            //return await _context.KeyWords.ToListAsync();
            return await _keyWordRepository.GetAllAsync();
        }

        // GET: api/KeyWords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KeyWordOutDTO>> GetKeyWord(Guid id)
        {
            //var keyWord = await _context.KeyWords.FindAsync(id);
            var keyWord = await _keyWordRepository.GetByIdAsync(id);
            if (keyWord == null)
            {
                return NotFound();
            }

            KeyWordOutDTO dto = new KeyWordOutDTO()
            {
                Id = keyWord.Id,
                Word = keyWord.Word

            };
            return dto;
        }

        // PUT: api/KeyWords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKeyWord(Guid id, KeyWordOutDTO keyWord)
        {
            if (id != keyWord.Id)
            {
                return BadRequest();
            }

            await _keyWordRepository.UpdateAsync(keyWord);

            return NoContent();
            /*_context.Entry(keyWord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KeyWordExists(id))
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

        // POST: api/KeyWords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KeyWordOutDTO>> PostKeyWord(KeyWordDTO keyWordDTO)
        {
            //_context.KeyWords.Add(keyWord);
            //await _context.SaveChangesAsync();
            var keyWord = new KeyWord()
            {
                Word = keyWordDTO.Word

            };
            Guid id = await _keyWordRepository.AddAsync(keyWord);
            return CreatedAtAction("GetKeyWord", new { id = id }, keyWord);
        }

        // DELETE: api/KeyWords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKeyWord(Guid id)
        {
            //var keyWord = await _context.KeyWords.FindAsync(id);
            var keyWord = await _keyWordRepository.GetByIdAsync(id);
            if (keyWord == null)
            {
                return NotFound();
            }

            //_context.KeyWords.Remove(keyWord);
            //await _context.SaveChangesAsync();
            await _keyWordRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool KeyWordExists(Guid id)
        {
            return _context.KeyWords.Any(e => e.Id == id);
        }
    }
}
