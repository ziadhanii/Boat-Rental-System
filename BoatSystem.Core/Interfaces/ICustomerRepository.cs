using BoatSystem.Core.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BoatSystem.Core.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(int id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(int id);
        Task<IEnumerable<Customer>> GetCustomersByUserIdAsync(string userId);
        Task<bool> AnyAsync(Expression<Func<Customer, bool>> predicate);
        Task<IEnumerable<Customer>> GetByUserIdAsync(string userId); 
        Task<int?> GetCustomerIdByUserIdAsync(string userId);

    }
}
