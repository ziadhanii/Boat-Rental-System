using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using BoatSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Infrastructure.Repositories
{
    public class BoatBookingRepository : IBoatBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BoatBookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BoatBooking> GetByIdAsync(int bookingId)
        {
            return await _context.BoatBookings.FindAsync(bookingId);
        }

        public async Task UpdateAsync(BoatBooking boatBooking)
        {
            _context.BoatBookings.Update(boatBooking);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(BoatBooking boatBooking)
        {
            _context.BoatBookings.Add(boatBooking);
            await _context.SaveChangesAsync();
        }
    }

}
