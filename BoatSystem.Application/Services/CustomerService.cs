using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BoatSystem.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<int?> GetCustomerIdByUserIdAsync(string userId)
        {
            return await _customerRepository.GetCustomerIdByUserIdAsync(userId);
        }
    }
}
