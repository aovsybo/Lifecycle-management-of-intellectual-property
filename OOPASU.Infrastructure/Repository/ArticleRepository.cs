using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OOPASU.Domain;
using OOPASU.Domain.DTO;

namespace OOPASU.Infrastructure.Repository
{
    public class ArticleRepository
    {
        private readonly Context _context;
        public ArticleRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<Article>> GetAllAsync()
        {
            return await _context.Articles.ToListAsync();
        }
        public async Task<Guid> AddAsync(ArticleDTO articleDTO)
        {
            var article = new Article()
            {
                CollectionName = articleDTO.CollectionName,
                CollectionNumber = articleDTO.CollectionNumber,
                CollectionPart = articleDTO.CollectionPart,
                FirstPage = articleDTO.FirstPage,
                LastPage = articleDTO.LastPage
            };
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
            return article.Id;
        }
        public async Task<ArticleOutDTO> GetByIdAsync(Guid id)
        {
            Article article = await _context.Articles.FindAsync(id);
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
        public async Task UpdateAsync(ArticleOutDTO article)
        {
            Article existArticle = await _context.Articles.FindAsync(article.Id);
            _context.Entry(existArticle).CurrentValues.SetValues(article);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            Article article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return false;
            }
            else
            {
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
                return true;
            }
        }

    }
}
