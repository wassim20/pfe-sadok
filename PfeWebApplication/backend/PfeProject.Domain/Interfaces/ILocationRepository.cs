using PfeProject.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PfeProject.Domain.Interfaces
{
    public interface ILocationRepository
    {
        // Legacy methods
        Task<IEnumerable<Location>> GetAllAsync(bool? isActive = true);
        Task<Location?> GetByIdAsync(int id);
        Task AddAsync(Location location);
        Task UpdateAsync(Location location);
        Task<bool> ExistsAsync(int id);
        Task SetActiveStatusAsync(int id, bool isActive);

        // Company-aware methods
        Task<IEnumerable<Location>> GetAllByCompanyAsync(int companyId, bool? isActive = true);
        Task<Location?> GetByIdAndCompanyAsync(int id, int companyId);
    }
}
