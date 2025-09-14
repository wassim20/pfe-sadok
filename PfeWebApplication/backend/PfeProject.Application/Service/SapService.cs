using PfeProject.Application.Interfaces;
using PfeProject.Application.Models.Saps;
using PfeProject.Domain.Entities;
using PfeProject.Domain.Interfaces;

namespace PfeProject.Application.Services
{
    public class SapService : ISapService
    {
        private readonly ISapRepository _repository;

        public SapService(ISapRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SapReadDto>> GetAllAsync(bool? isActive = true)
        {
            var saps = await _repository.GetAllAsync(isActive);
            return saps.Select(s => new SapReadDto
            {
                Id = s.Id,
                Article = s.Article,
                UsCode = s.UsCode,
                Quantite = s.Quantite,
                IsActive = s.IsActive
            });
        }

        public async Task<SapReadDto?> GetByIdAsync(int id)
        {
            var sap = await _repository.GetByIdAsync(id);
            if (sap == null)
                return null;

            return new SapReadDto
            {
                Id = sap.Id,
                Article = sap.Article,
                UsCode = sap.UsCode,
                Quantite = sap.Quantite,
                IsActive = sap.IsActive
            };
        }

        public async Task CreateAsync(SapCreateDto dto)
        {
            var sap = new Sap
            {
                Article = dto.Article,
                UsCode = dto.UsCode,
                Quantite = dto.Quantite,
                IsActive = true
            };

            await _repository.AddAsync(sap);
        }

        public async Task<bool> UpdateAsync(int id, SapUpdateDto dto)
        {
            var sap = await _repository.GetByIdAsync(id);
            if (sap == null)
                return false;

            sap.Article = dto.Article;
            sap.UsCode = dto.UsCode;
            sap.Quantite = dto.Quantite;

            await _repository.UpdateAsync(sap);
            return true;
        }

        public async Task<bool> SetActiveStatusAsync(int id, bool isActive)
        {
            var exists = await _repository.ExistsAsync(id);
            if (!exists)
                return false;

            await _repository.SetActiveStatusAsync(id, isActive);
            return true;
        }
    }
}
