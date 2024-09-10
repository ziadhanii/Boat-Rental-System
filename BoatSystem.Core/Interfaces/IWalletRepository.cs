using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Core.Interfaces
{
    public interface IWalletRepository
    {
        Task<decimal> GetBalanceAsync(int customerId);
        Task<bool> AddFundsAsync(int customerId, decimal amount);
    }

}
