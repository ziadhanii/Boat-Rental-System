using BoatSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoatSystem.Core.Repositories
{
    public interface IAdditionalServiceRepository
    {
        Task<List<Addition>> GetAllAsync();
        Task<Addition> GetByIdAsync(int id);
        Task<IEnumerable<Addition>> GetByOwnerIdAsync(int ownerId);
        Task<Addition> AddAsync(Addition addition);
        Task<bool> UpdateAsync(Addition addition);
        Task<bool> DeleteAsync(int id);
    }
}
