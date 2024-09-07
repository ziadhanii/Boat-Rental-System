using BoatSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoatSystem.Core.Interfaces
{
    public interface ITripRepository
    {
        Task<Trip> GetByIdAsync(int id);
        Task<IEnumerable<Trip>> GetAllAsync();
        Task AddAsync(Trip trip);
        Task UpdateAsync(Trip trip);
        Task DeleteAsync(int id);
        Task<IEnumerable<Trip>> GetTripsByBoatIdAsync(int boatId);
    }
}
