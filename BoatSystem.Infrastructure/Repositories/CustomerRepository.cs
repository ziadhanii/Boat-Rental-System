using BoatSystem.Core.Entities;
using BoatSystem.Infrastructure.Data;
using BoatSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BoatSystem.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await GetByIdAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> AnyAsync(Expression<Func<Customer, bool>> predicate)
        {
            return await _context.Customers.AnyAsync(predicate);
        }
        public async Task<IEnumerable<Customer>> GetCustomersByUserIdAsync(string userId)
        {
            return await _context.Customers.Where(c => c.UserId == userId).ToListAsync();
        }
        public async Task<IEnumerable<Customer>> GetByUserIdAsync(string userId)
        {
            return await _context.Customers.Where(c => c.UserId == userId).ToListAsync();
        }


    }
}
