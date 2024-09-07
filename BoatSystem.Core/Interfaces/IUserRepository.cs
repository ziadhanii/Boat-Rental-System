using BoatSystem.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoatSystem.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task AddAsync(ApplicationUser user);
        Task<ApplicationUser> GetByIdAsync(string id);
        Task UpdateAsync(ApplicationUser user);
        Task DeleteAsync(string id);
    }
}
