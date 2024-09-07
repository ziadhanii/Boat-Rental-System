using BoatSystem.Core.Entities;
using System.Collections.Generic;
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
        Task<IEnumerable<Customer>> GetCustomersByUserIdAsync(string userId);  // تأكد من توافق النوع هنا أيضًا
        Task<bool> AnyAsync(System.Linq.Expressions.Expression<System.Func<Customer, bool>> predicate);
        Task<IEnumerable<Customer>> GetByUserIdAsync(string userId);
    }
}
