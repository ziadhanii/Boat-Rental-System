using BoatSystem.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoatSystem.Core.Interfaces
{
    public interface ITripService
    {
        Task<TripDetailsDto> AddTripAsync(CreateTripDto tripDto);
        Task<TripDetailsDto> UpdateTripAsync(UpdateTripDto tripDto);
        Task<bool> DeleteTripAsync(int id, int ownerId);
        Task<TripDetailsDto> GetTripByIdAsync(int id);
        Task<IEnumerable<TripSummaryDto>> GetTripsByOwnerIdAsync(int ownerId);
        Task<int?> GetOwnerIdByUserIdAsync(string userId);

    }
}
