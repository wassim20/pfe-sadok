using PfeProject.Application.Interfaces;
using PfeProject.Application.Models.Articles;
using PfeProject.Domain.Entities;
using PfeProject.Domain.Interfaces;

namespace PfeProject.Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _repo;

        public ArticleService(IArticleRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ArticleReadDto>> GetAllAsync(bool? isActive = true)
        {
            var list = await _repo.GetAllAsync(isActive);
            return list.Select(a => new ArticleReadDto
            {
                Id = a.Id,
                CodeProduit = a.CodeProduit,
                Designation = a.Designation,
                DateAjout = a.DateAjout,
                IsActive = a.IsActive
            });
        }

        public async Task<ArticleReadDto?> GetByIdAsync(int id)
        {
            var a = await _repo.GetByIdAsync(id);
            if (a == null) return null;
            return new ArticleReadDto
            {
                Id = a.Id,
                CodeProduit = a.CodeProduit,
                Designation = a.Designation,
                DateAjout = a.DateAjout,
                IsActive = a.IsActive
            };
        }

        public async Task<ArticleReadDto> CreateAsync(ArticleCreateDto dto)
        {
            var article = new Article
            {
                CodeProduit = dto.CodeProduit,
                Designation = dto.Designation,
                IsActive = true,
                DateAjout = DateTime.UtcNow
            };
            await _repo.AddAsync(article);

            return new ArticleReadDto
            {
                Id = article.Id,
                CodeProduit = article.CodeProduit,
                Designation = article.Designation,
                DateAjout = article.DateAjout,
                IsActive = article.IsActive
            };
        }

        public async Task<bool> UpdateAsync(int id, ArticleUpdateDto dto)
        {
            var article = await _repo.GetByIdAsync(id);
            if (article == null) return false;

            article.CodeProduit = dto.CodeProduit;
            article.Designation = dto.Designation;
            await _repo.UpdateAsync(article);
            return true;
        }

        public async Task<bool> SetActiveStatusAsync(int id, bool isActive)
        {
            return await _repo.SetActiveStatusAsync(id, isActive);
        }
    }
}
