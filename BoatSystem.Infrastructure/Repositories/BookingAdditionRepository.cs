using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using BoatSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoatSystem.Infrastructure.Repositories
{
    public class BookingAdditionRepository : IBookingAdditionRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingAdditionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookingAddition>> GetByIdsAsync(List<int> additionIds)
        {
            return await _context.BookingAdditions
                 .Where(ba => additionIds.Contains(ba.AdditionId))
                 .ToListAsync();
        }

        public async Task AddRangeAsync(IEnumerable<BookingAddition> bookingAdditions)
        {
            await _context.BookingAdditions.AddRangeAsync(bookingAdditions);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(BookingAddition bookingAddition)
        {
            await _context.BookingAdditions.AddAsync(bookingAddition);
            await _context.SaveChangesAsync();
        }

    }
}
