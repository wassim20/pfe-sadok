using PfeProject.Application.Interfaces;
using PfeProject.Domain.Entities;
using PfeProject.Domain.Interfaces;

namespace PfeProject.Application.Services
{
    public class MovementTraceService : IMovementTraceService
    {
        private readonly IMovementTraceRepository _repository;

        public MovementTraceService(IMovementTraceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MovementTraceReadDto>> GetAllAsync(bool? isActive = true)
        {
            var list = await _repository.GetAllAsync(isActive);
            return list.Select(mt => new MovementTraceReadDto
            {
                Id = mt.Id,
                UsNom = mt.UsNom,
                Quantite = mt.Quantite,
                DateMouvement = mt.DateMouvement,
                UserId = mt.UserId,
                DetailPicklistId = mt.DetailPicklistId,
                IsActive = mt.IsActive
            });
        }

        public async Task<MovementTraceReadDto?> GetByIdAsync(int id)
        {
            var mt = await _repository.GetByIdAsync(id);
            if (mt == null) return null;

            return new MovementTraceReadDto
            {
                Id = mt.Id,
                UsNom = mt.UsNom,
                Quantite = mt.Quantite,
                DateMouvement = mt.DateMouvement,
                UserId = mt.UserId,
                DetailPicklistId = mt.DetailPicklistId,
                IsActive = mt.IsActive
            };
        }

        public async Task<MovementTraceReadDto> CreateAsync(MovementTraceCreateDto dto)
        {
            var mt = new MovementTrace
            {
                UsNom = dto.UsNom,
                Quantite = dto.Quantite,
                UserId = dto.UserId,
                DetailPicklistId = dto.DetailPicklistId,
                IsActive = true
            };

            await _repository.AddAsync(mt);

            return new MovementTraceReadDto
            {
                Id = mt.Id,
                UsNom = mt.UsNom,
                Quantite = mt.Quantite,
                DateMouvement = mt.DateMouvement,
                UserId = mt.UserId,
                DetailPicklistId = mt.DetailPicklistId,
                IsActive = mt.IsActive
            };
        }

        public async Task<bool> SetActiveStatusAsync(int id, bool isActive)
        {
            var exists = await _repository.ExistsAsync(id);
            if (!exists) return false;

            return await _repository.SetActiveStatusAsync(id, isActive);
        }
    }
}
