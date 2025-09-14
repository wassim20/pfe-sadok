using PfeProject.Application.Models.Articles;

namespace PfeProject.Application.Interfaces
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleReadDto>> GetAllAsync(bool? isActive = true);
        Task<ArticleReadDto?> GetByIdAsync(int id);
        Task<ArticleReadDto> CreateAsync(ArticleCreateDto dto);
        Task<bool> UpdateAsync(int id, ArticleUpdateDto dto);
        Task<bool> SetActiveStatusAsync(int id, bool isActive);
    }
}
