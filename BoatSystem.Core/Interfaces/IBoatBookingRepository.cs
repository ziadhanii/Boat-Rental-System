using BoatSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Core.Interfaces
{
    public interface IBoatBookingRepository
    {
        Task<BoatBooking> GetByIdAsync(int bookingId);
        Task UpdateAsync(BoatBooking boatBooking);
        Task AddAsync(BoatBooking boatBooking);

    }

}
