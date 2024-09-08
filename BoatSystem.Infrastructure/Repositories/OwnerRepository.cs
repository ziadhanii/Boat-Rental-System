using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using BoatSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoatSystem.Infrastructure.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly ApplicationDbContext _context;

        public OwnerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Owner> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Owners.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework like Serilog)
                throw new Exception("An error occurred while fetching the owner.", ex);
            }
        }

        public async Task<IEnumerable<Owner>> GetAllAsync()
        {
            try
            {
                return await _context.Owners.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while fetching all owners.", ex);
            }
        }

        public async Task AddAsync(Owner owner)
        {
            try
            {
                await _context.Owners.AddAsync(owner);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while adding the owner.", ex);
            }
        }

        public async Task UpdateAsync(Owner owner)
        {
            try
            {
                _context.Owners.Update(owner);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while updating the owner.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var owner = await GetByIdAsync(id);
                if (owner != null)
                {
                    _context.Owners.Remove(owner);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while deleting the owner.", ex);
            }
        }

        public async Task<IEnumerable<Owner>> GetOwnersByUserIdAsync(string userId)
        {
            try
            {
                return await _context.Owners.Where(o => o.UserId == userId).ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while fetching owners by user ID.", ex);
            }
        }

        public async Task<Owner> GetByUserIdAsync(string userId)
        {
            try
            {

                return await _context.Owners.FirstOrDefaultAsync(o => o.UserId == userId);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while fetching the owner by user ID.", ex);
            }
        }
    }
}
