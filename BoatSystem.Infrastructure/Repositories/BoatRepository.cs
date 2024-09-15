using BoatSystem.Core.Entities;
using BoatSystem.Infrastructure.Data;
using BoatSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoatSystem.Core.DTOs;

namespace BoatSystem.Infrastructure.Repositories
{
    public class BoatRepository : IBoatRepository
    {
        private readonly ApplicationDbContext _context;

        public BoatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Boat> GetByIdAsync(int id)
        {
            return await _context.Boats.FindAsync(id);
        }

        public async Task<IEnumerable<Boat>> GetAllAsync()
        {
            return await _context.Boats.ToListAsync();
        }

        public async Task AddAsync(Boat boat)
        {
            await _context.Boats.AddAsync(boat);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Boat boat)
        {
            _context.Boats.Update(boat);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var boat = await GetByIdAsync(id);
            if (boat != null)
            {
                _context.Boats.Remove(boat);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Boat>> GetBoatsByOwnerIdAsync(int ownerId)
        {
            return await _context.Boats.Where(b => b.OwnerId == ownerId).ToListAsync();
        }

        public async Task<IEnumerable<Boat>> GetBoatsByNameAsync(string name)
        {
            return await _context.Boats
                .Where(b => b.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<Boat>> GetUnapprovedBoatsAsync()
        {
            return await _context.Boats
                .Where(b => b.IsApproved == false)
                .ToListAsync();
        }
        public async Task<IEnumerable<BoatDto>> GetAvailableBoatsAsync()
        {
            return await _context.Boats
                .Where(b => b.IsApproved && b.Status == "Available") 
                .Select(b => new BoatDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Description = b.Description,
                    Capacity = b.Capacity,
                    ReservationPrice = b.ReservationPrice,
                    OwnerId = b.OwnerId
                })
                .ToListAsync();
        }


    }
}
