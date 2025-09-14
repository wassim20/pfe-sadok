using Microsoft.EntityFrameworkCore;
using PfeProject.Domain.Entities;
using PfeProject.Domain.Interfaces;
using PfeProject.Infrastructure.Persistence;

namespace PfeProject.Infrastructure.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Article>> GetAllAsync(bool? isActive = true)
        {
            var query = _context.Articles.AsQueryable();
            if (isActive.HasValue)
                query = query.Where(a => a.IsActive == isActive.Value);
            return await query.ToListAsync();
        }

        public async Task<Article?> GetByIdAsync(int id)
        {
            return await _context.Articles.FindAsync(id);
        }

        public async Task AddAsync(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Article article)
        {
            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Articles.AnyAsync(a => a.Id == id);
        }

        public async Task<bool> SetActiveStatusAsync(int id, bool isActive)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null) return false;
            article.IsActive = isActive;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
