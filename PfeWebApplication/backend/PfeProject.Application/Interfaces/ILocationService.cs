using PfeProject.Application.Models.Locations;

namespace PfeProject.Application.Interfaces
{
    public interface ILocationService
    {
        // Legacy methods
        Task<IEnumerable<LocationReadDto>> GetAllAsync(bool? isActive = true);
        Task<LocationReadDto?> GetByIdAsync(int id);
        Task CreateAsync(LocationCreateDto dto);
        Task<bool> UpdateAsync(int id, LocationUpdateDto dto);
        Task<bool> SetActiveStatusAsync(int id, bool isActive);

        // Company-aware methods
        Task<IEnumerable<LocationReadDto>> GetAllByCompanyAsync(int companyId, bool? isActive = true);
        Task<LocationReadDto?> GetByIdAndCompanyAsync(int id, int companyId);
        Task CreateForCompanyAsync(LocationCreateDto dto, int companyId);
        Task<bool> UpdateForCompanyAsync(int id, LocationUpdateDto dto, int companyId);
    }
}
