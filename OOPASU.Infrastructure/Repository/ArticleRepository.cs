using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OOPASU.Domain;
using OOPASU.Domain.DTO;
using OOPASU.Infrastructure.Data;

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
        public async Task<Guid> AddAsync(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
            return article.Id;
        }
        public async Task<Article> GetByIdAsync(Guid id)
        {
            Article article = await _context.Articles.FindAsync(id);
            
            return article;
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
