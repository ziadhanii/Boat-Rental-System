// Core/Interfaces/ICancellationRepository.cs
using BoatSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoatSystem.Core.Interfaces
{
    public interface ICancellationRepository
    {
        Task AddAsync(Cancellation cancellation);
        Task<List<Cancellation>> GetCancellationsByCustomerIdAsync(int customerId);
    }
}
