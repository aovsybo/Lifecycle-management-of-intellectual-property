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
    public class ArticlesController : ControllerBase
    {
        private readonly Context _context;
        private readonly ArticleRepository _articleRepository;
        public ArticlesController(Context context)
        {
            _context = context;
            _articleRepository = new ArticleRepository(_context);
        }

        // GET: api/Articles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
            //return await _context.Articles.ToListAsync();
            return await _articleRepository.GetAllAsync();
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleOutDTO>> GetArticle(Guid id)
        {
            //var article = await _context.Articles.FindAsync(id);
            var article = await _articleRepository.GetByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            ArticleOutDTO dto = new ArticleOutDTO()
            {
                Id = article.Id,
                CollectionName = article.CollectionName,
                CollectionNumber = article.CollectionNumber,
                CollectionPart = article.CollectionPart,
                FirstPage = article.FirstPage,
                LastPage = article.LastPage
            };

            return dto;
        }

        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(Guid id, ArticleOutDTO article)
        {
            if (id != article.Id)
            {
                return BadRequest();
            }

            await _articleRepository.UpdateAsync(article);

            return NoContent();
            /*_context.Entry(article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
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

        // POST: api/Articles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArticleOutDTO>> PostArticle(ArticleDTO articleDTO)
        {
            //_context.Articles.Add(article);
            //await _context.SaveChangesAsync();

            var article = new Article()
            {
                CollectionName = articleDTO.CollectionName,
                CollectionNumber = articleDTO.CollectionNumber,
                CollectionPart = articleDTO.CollectionPart,
                FirstPage = articleDTO.FirstPage,
                LastPage = articleDTO.LastPage
            };

            Guid id = await _articleRepository.AddAsync(article);
            return CreatedAtAction("GetArticle", new { id = id }, article);
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(Guid id)
        {
            //var article = await _context.Articles.FindAsync(id);
            var article = await _articleRepository.GetByIdAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            //_context.Articles.Remove(article);
            //await _context.SaveChangesAsync();
            await _articleRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool ArticleExists(Guid id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
