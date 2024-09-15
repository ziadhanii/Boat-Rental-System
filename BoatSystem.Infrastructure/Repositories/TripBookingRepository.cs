using BoatSystem.Core.Entities;
using BoatSystem.Infrastructure.Data;
using System.Threading.Tasks;

namespace BoatSystem.Core.Interfaces
{
    public interface ITripBookingRepository
    {
        Task AddAsync(TripBooking tripBooking);
    }

    public class TripBookingRepository : ITripBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public TripBookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TripBooking tripBooking)
        {
            _context.TripBookings.Add(tripBooking);
            await _context.SaveChangesAsync();
        }

    }
}
