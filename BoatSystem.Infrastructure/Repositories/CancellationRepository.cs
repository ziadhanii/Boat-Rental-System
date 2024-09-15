using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using BoatSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoatSystem.Infrastructure.Repositories
{
    public class CancellationRepository : ICancellationRepository
    {
        private readonly ApplicationDbContext _context;

        public CancellationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cancellation>> GetCancellationsByCustomerIdAsync(int customerId)
        {
            return await _context.Cancellations
                .Where(c => c.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task AddAsync(Cancellation cancellation)
        {
            await _context.Cancellations.AddAsync(cancellation);
            await _context.SaveChangesAsync();
        }
    }
}
