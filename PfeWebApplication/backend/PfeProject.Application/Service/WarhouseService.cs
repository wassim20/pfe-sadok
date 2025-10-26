using PfeProject.Application.Interfaces;
using PfeProject.Application.Models.Warehouses;
using PfeProject.Domain.Entities;
using PfeProject.Domain.Interfaces;

namespace PfeProject.Application.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _repository;

        public WarehouseService(IWarehouseRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<WarehouseReadDto>> GetAllAsync(bool? isActive = true)
        {
            var warehouses = await _repository.GetAllAsync(isActive);

            return warehouses.Select(w => new WarehouseReadDto
            {
                Id = w.Id,
                Name = w.Name,
                Description = w.Description,
                IsActive = w.IsActive
            });
        }

        public async Task<WarehouseReadDto?> GetByIdAsync(int id)
        {
            var warehouse = await _repository.GetByIdAsync(id);
            if (warehouse == null || !warehouse.IsActive)
                return null;

            return new WarehouseReadDto
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                Description = warehouse.Description,
                IsActive = warehouse.IsActive
            };
        }

        public async Task CreateAsync(WarehouseCreateDto dto)
        {
            var warehouse = new Warehouse
            {
                Name = dto.Name,
                Description = dto.Description,
                IsActive = true
            };

            await _repository.AddAsync(warehouse);
        }

        public async Task<bool> UpdateAsync(int id, WarehouseUpdateDto dto)
        {
            var warehouse = await _repository.GetByIdAsync(id);
            if (warehouse == null || !warehouse.IsActive)
                return false;

            warehouse.Name = dto.Name;
            warehouse.Description = dto.Description;

            await _repository.UpdateAsync(warehouse);
            return true;
        }

        public async Task<IEnumerable<WarehouseReadDto>> GetAllByCompanyAsync(int companyId, bool? isActive = true)
        {
            var warehouses = await _repository.GetAllByCompanyAsync(companyId, isActive);

            return warehouses.Select(w => new WarehouseReadDto
            {
                Id = w.Id,
                Name = w.Name,
                Description = w.Description,
                IsActive = w.IsActive
            });
        }

        public async Task<WarehouseReadDto?> GetByIdAndCompanyAsync(int id, int companyId)
        {
            var warehouse = await _repository.GetByIdAndCompanyAsync(id, companyId);
            if (warehouse == null || !warehouse.IsActive)
                return null;

            return new WarehouseReadDto
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                Description = warehouse.Description,
                IsActive = warehouse.IsActive
            };
        }

        public async Task CreateForCompanyAsync(WarehouseCreateDto dto, int companyId)
        {
            var warehouse = new Warehouse
            {
                Name = dto.Name,
                Description = dto.Description,
                IsActive = true,
                CompanyId = companyId  // 🏢 Set Company relationship
            };

            await _repository.AddAsync(warehouse);
        }

        public async Task<bool> UpdateForCompanyAsync(int id, WarehouseUpdateDto dto, int companyId)
        {
            var warehouse = await _repository.GetByIdAndCompanyAsync(id, companyId);
            if (warehouse == null || !warehouse.IsActive)
                return false;

            warehouse.Name = dto.Name;
            warehouse.Description = dto.Description;

            await _repository.UpdateAsync(warehouse);
            return true;
        }

        public async Task<bool> SetActiveStatusAsync(int id, bool isActive)
        {
            var exists = await _repository.ExistsAsync(id);
            if (!exists)
                return false;

            // Note: This method doesn't need company filtering as it's a simple status update
            // The repository should handle the actual status update
            var warehouse = await _repository.GetByIdAsync(id);
            if (warehouse == null)
                return false;

            warehouse.IsActive = isActive;
            await _repository.UpdateAsync(warehouse);
            return true;
        }
    }
}
        
