using BoatSystem.Core.Entities;
using System.Threading.Tasks;

namespace BoatSystem.Core.Interfaces
{
    public interface IAdminService
    {
        Task ApproveUserRegistrationAsync(string userId);
        Task RejectUserRegistrationAsync(string userId);
        Task ApproveBoatRegistrationAsync(int boatId);
        Task RejectBoatRegistrationAsync(int boatId);
        Task<IEnumerable<Reservation>> MonitorReservationsAsync();
    }
}
