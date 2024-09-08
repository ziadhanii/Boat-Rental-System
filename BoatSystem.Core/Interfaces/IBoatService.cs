using System.Collections.Generic;
using System.Threading.Tasks;
using BoatSystem.Core.DTOs;

namespace BoatSystem.Core.Interfaces
{
    public interface IBoatService
    {
        Task<int> AddBoatAsync(BoatDto boatDto);
        Task<IEnumerable<BoatSummaryDto>> GetBoatsByNameAsync(string name);
        Task<IEnumerable<BoatApprovalDto>> GetUnapprovedBoatsAsync();
        Task<BoatDetailsDto> GetBoatByIdAsync(int id);
    }
}
