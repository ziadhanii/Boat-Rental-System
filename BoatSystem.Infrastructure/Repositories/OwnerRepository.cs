//using BoatSystem.Core.Entities;
//using BoatSystem.Infrastructure.Data;
//using BoatSystem.Core.Interfaces;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using System.Linq;
//using BoatRentalSystem.Core.Interfaces;

//namespace BoatSystem.Infrastructure.Repositories
//{
//    public class OwnerRepository : IOwnerRepository
//    {
//        private readonly ApplicationDbContext _context;

//        public OwnerRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<Owner> GetByIdAsync(int id)
//        {
//            return await _context.Owners.FindAsync(id);
//        }

//        public async Task<IEnumerable<Owner>> GetAllAsync()
//        {
//            return await _context.Owners.ToListAsync();
//        }

//        public async Task AddAsync(Owner owner)
//        {
//            await _context.Owners.AddAsync(owner);
//            await _context.SaveChangesAsync();
//        }

//        public async Task UpdateAsync(Owner owner)
//        {
//            _context.Owners.Update(owner);
//            await _context.SaveChangesAsync();
//        }

//        public async Task DeleteAsync(int id)
//        {
//            var owner = await GetByIdAsync(id);
//            if (owner != null)
//            {
//                _context.Owners.Remove(owner);
//                await _context.SaveChangesAsync();
//            }
//        }

//        public async Task<IEnumerable<Owner>> GetOwnersByUserIdAsync(int userId)
//        {
//            return await _context.Owners.Where(o => o.UserId == userId).ToListAsync();
//        }
//    }
//}
