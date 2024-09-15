using BoatSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoatSystem.Core.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking> GetByIdAsync(int bookingId);
        Task AddAsync(BoatBooking booking);
        Task<BoatBooking> BookTripAsync(int customerId, int tripId, int participants, List<int> additionalServiceIds);
        Task<BoatBooking> BookBoatAsync(int customerId, int boatId, List<int> serviceIds, string purpose);
        Task<decimal> CalculateTotalCostAsync(int bookingId);
        Task<bool> CancelBookingAsync(int bookingId);
        Task<IEnumerable<BoatBooking>> GetBookingHistoryAsync(int customerId);
        Task<List<Booking>> GetBookingsByCustomerIdAsync(int customerId);
        Task UpdateAsync(Booking booking); 

    }
}
