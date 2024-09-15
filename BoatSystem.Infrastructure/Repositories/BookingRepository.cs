using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using BoatSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoatSystem.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(BoatBooking booking)
        {
            _context.BoatBookings.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<BoatBooking> BookTripAsync(int customerId, int tripId, int participants, List<int> additionalServiceIds)
        {
            var trip = await _context.Trips.FindAsync(tripId);
            if (trip == null) throw new InvalidOperationException("Trip not found");

            var booking = new BoatBooking
            {
                CustomerId = customerId,
                TripId = tripId,
                NumberOfPeople = participants,
                BookingDate = DateTime.UtcNow,
                DurationHours = trip.DurationHours, 
                TotalPrice = trip.PricePerPerson * participants,
                Status = "Booked"
            };

            _context.BoatBookings.Add(booking);
            await _context.SaveChangesAsync();

            if (additionalServiceIds.Any())
            {
                var additionalServices = await _context.BookingAdditions
                    .Where(sa => additionalServiceIds.Contains(sa.Id))
                    .ToListAsync();
                foreach (var service in additionalServices)
                {
                    booking.BookingAdditions.Add(service);
                }

                booking.TotalPrice += additionalServices.Sum(sa => sa.TotalPrice);
                await _context.SaveChangesAsync();
            }

            return booking;
        }

        public async Task<BoatBooking> BookBoatAsync(int customerId, int boatId, List<int> serviceIds, string purpose)
        {
            var booking = new BoatBooking
            {
                CustomerId = customerId,
                BoatId = boatId,
                TripId = null, 
                NumberOfPeople = 0, 
                BookingDate = DateTime.UtcNow,
                DurationHours = 0, 
                TotalPrice = 0, 
                Status = "Booked"
            };

            _context.BoatBookings.Add(booking);
            await _context.SaveChangesAsync();

            if (serviceIds.Any())
            {
                var additionalServices = await _context.BookingAdditions
                    .Where(sa => serviceIds.Contains(sa.Id))
                    .ToListAsync();
                foreach (var service in additionalServices)
                {
                    booking.BookingAdditions.Add(service);
                }

                booking.TotalPrice += additionalServices.Sum(sa => sa.TotalPrice);
                await _context.SaveChangesAsync();
            }

            return booking;
        }

        public async Task<decimal> CalculateTotalCostAsync(int bookingId)
        {
            var booking = await _context.BoatBookings
                .Include(b => b.BookingAdditions)
                .FirstOrDefaultAsync(b => b.Id == bookingId);

            if (booking == null)
                throw new InvalidOperationException("Booking not found");

            decimal totalCost = booking.TotalPrice;

            if (booking.BookingAdditions != null)
            {
                totalCost += booking.BookingAdditions.Sum(ba => ba.TotalPrice);
            }

            return totalCost;
        }

        public async Task<bool> CancelBookingAsync(int bookingId)
        {
            var booking = await _context.BoatBookings.FindAsync(bookingId);

            if (booking == null) return false;

            booking.Status = "Canceled";
            booking.CanceledAt = DateTime.UtcNow;
            _context.BoatBookings.Update(booking);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<BoatBooking>> GetBookingHistoryAsync(int customerId)
        {
            return await _context.BoatBookings
                .Where(b => b.CustomerId == customerId)
                .ToListAsync();
        }


        public async Task<List<Booking>> GetBookingsByCustomerIdAsync(int customerId)
        {
            return await _context.Bookings
                .Where(b => b.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<Booking> GetByIdAsync(int bookingId)
        {
            return await _context.Bookings.FindAsync(bookingId);
        }

        public async Task UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }
    }
}
