using BoatSystem.Core.Entities;
using BoatSystem.Core.Repositories;
using BoatSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoatSystem.Infrastructure.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly ApplicationDbContext _context;

        public TripRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Trip> AddAsync(Trip trip)
        {
            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();
            return trip;
        }

        public async Task<bool> UpdateAsync(Trip trip)
        {
            _context.Trips.Update(trip);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
            {
                return false;
            }

            _context.Trips.Remove(trip);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Trip> GetByIdAsync(int id)
        {
            return await _context.Trips.FindAsync(id);
        }

        public async Task<IEnumerable<Trip>> GetByOwnerIdAsync(int ownerId)
        {
            return await _context.Trips.Where(t => t.OwnerId == ownerId).ToListAsync();
        }

        public async Task<IEnumerable<Trip>> GetAvailableTripsAsync()
        {
            return await _context.Trips
                .Where(trip => trip.Status == "Available") // تأكد من أن حالة الرحلة هي "Available"
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int tripId)
        {
            return await _context.Trips.AnyAsync(t => t.Id == tripId);
        }

        public async Task<Boat> GetBoatByTripIdAsync(int tripId)
        {
            // الحصول على القارب المرتبط بالرحلة
            var trip = await _context.Trips
                .Include(t => t.Boat)
                .FirstOrDefaultAsync(t => t.Id == tripId);

            return trip?.Boat;
        }
    }
}
