using System;
using System.Threading.Tasks;

namespace BoatSystem.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<int?> GetCustomerIdByUserIdAsync(string userId);
    }
}
