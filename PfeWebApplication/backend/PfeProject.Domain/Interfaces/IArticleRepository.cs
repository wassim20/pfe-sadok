using PfeProject.Domain.Entities;

namespace PfeProject.Domain.Interfaces
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAllAsync(bool? isActive = true);
        Task<Article?> GetByIdAsync(int id);
        Task AddAsync(Article article);
        Task UpdateAsync(Article article);
        Task<bool> ExistsAsync(int id);
        Task<bool> SetActiveStatusAsync(int id, bool isActive);
    }
}
