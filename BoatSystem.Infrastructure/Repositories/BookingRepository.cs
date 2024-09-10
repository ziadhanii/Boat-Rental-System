//using BoatSystem.Core.Entities;
//using BoatSystem.Core.Interfaces;
//using BoatSystem.Infrastructure.Data;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace BoatSystem.Infrastructure.Repositories
//{
//    public class BookingRepository : IBookingRepository
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly ILogger<BookingRepository> _logger;

//        public BookingRepository(ApplicationDbContext context, ILogger<BookingRepository> logger)
//        {
//            _context = context;
//            _logger = logger;
//        }

//        public async Task<BoatBooking> GetByIdAsync(int id)
//        {
//            return await _context.BoatBookings
//                .Include(b => b.BookingAdditions)
//                .ThenInclude(ba => ba.Addition)
//                .FirstOrDefaultAsync(b => b.Id == id);
//        }

//        public async Task CreateAsync(BoatBooking booking)
//        {
//            // تحقق من أن CustomerId موجود
//            var customerExists = await _context.Customers
//                .AnyAsync(c => c.Id == booking.CustomerId);

//            if (!customerExists)
//            {
//                throw new InvalidOperationException("Customer does not exist.");
//            }

//            // تحقق من أن BoatId موجود
//            var boatExists = await _context.Boats
//                .AnyAsync(b => b.Id == booking.BoatId);

//            if (!boatExists)
//            {
//                throw new InvalidOperationException("Boat does not exist.");
//            }

//            // تحقق من أن TripId موجود
//            var tripExists = await _context.Trips
//                .AnyAsync(t => t.Id == booking.TripId);

//            if (!tripExists)
//            {
//                throw new InvalidOperationException("Trip does not exist.");
//            }

//            // إضافة الحجز إلى قاعدة البيانات
//            _context.BoatBookings.Add(booking);
//            await _context.SaveChangesAsync();
//        }

//        public async Task<IEnumerable<BoatBooking>> GetBookingsByCustomerIdAsync(int customerId)
//        {
//            return await _context.BoatBookings
//                .Where(b => b.CustomerId == customerId)
//                .Include(b => b.BookingAdditions)
//                .ThenInclude(ba => ba.Addition)
//                .ToListAsync();
//        }

//        public async Task<IEnumerable<BoatBooking>> GetBookingsByTripIdAsync(int tripId)
//        {
//            return await _context.BoatBookings
//                .Where(b => b.TripId == tripId)
//                .Include(b => b.BookingAdditions)
//                .ThenInclude(ba => ba.Addition)
//                .ToListAsync();
//        }
//    }
//}
