using BoatSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoatSystem.Core.Interfaces
{
    public interface IReservationRepository
    {
        Task<Reservation> GetByIdAsync(int id);
        Task<IEnumerable<Reservation>> GetAllAsync();
        Task AddAsync(Reservation reservation);
        Task UpdateAsync(Reservation reservation);
        Task DeleteAsync(int id);
        Task<IEnumerable<Reservation>> GetReservationsByTripIdAsync(int tripId);
        Task<IEnumerable<Reservation>> GetAllIncludingDetailsAsync();
        Task<IEnumerable<Reservation>> GetReservationsByBoatIdAsync(int boatId); // New method
    }
}
