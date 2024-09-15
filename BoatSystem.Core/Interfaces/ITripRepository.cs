using BoatSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoatSystem.Core.Repositories
{
    public interface ITripRepository
    {
        Task<Trip> AddAsync(Trip trip);
        Task<bool> UpdateAsync(Trip trip);
        Task<bool> DeleteAsync(int id);
        Task<Trip> GetByIdAsync(int id);
        Task<IEnumerable<Trip>> GetByOwnerIdAsync(int ownerId);
        Task<IEnumerable<Trip>> GetAvailableTripsAsync();
        Task<bool> ExistsAsync(int tripId);
        Task<Boat> GetBoatByTripIdAsync(int tripId); 
    }
}
