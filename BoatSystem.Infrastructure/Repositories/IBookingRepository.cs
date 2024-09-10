using BoatSystem.Core.Entities;

namespace BoatSystem.Infrastructure.Repositories
{
    public interface IBookingRepository
    {
        Task<BoatBooking> BookBoatAsync(int customerId, int boatId, string purpose);
        Task<BoatBooking> BookTripAsync(int customerId, int tripId, int participants);
        Task<bool> CancelBookingAsync(int bookingId);
        Task<IEnumerable<BoatBooking>> GetCustomerBookingHistoryAsync(int customerId);
    }
}