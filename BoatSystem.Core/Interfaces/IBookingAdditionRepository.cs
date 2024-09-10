using BoatSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Core.Interfaces
{
    public interface IBookingAdditionRepository
    {
        Task<List<BookingAddition>> GetByIdsAsync(List<int> additionIds);
        Task AddAsync(BookingAddition bookingAddition); // Add this method
        Task AddRangeAsync(IEnumerable<BookingAddition> bookingAdditions); // Optional if you also need AddRangeAsync
    }
}
