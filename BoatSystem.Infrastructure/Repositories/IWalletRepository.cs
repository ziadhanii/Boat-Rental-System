using System.Threading.Tasks;

namespace BoatSystem.Core.Interfaces
{
    public interface IWalletRepository
    {
        Task<decimal> GetWalletBalanceAsync(int customerId);
        Task<bool> AddFundsAsync(int customerId, decimal amount);
    }
}
