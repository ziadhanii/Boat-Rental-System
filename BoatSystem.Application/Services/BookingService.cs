//using BoatSystem.Core.Entities;
//using BoatSystem.Core.Interfaces;
//using BoatSystem.Core.Repositories;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace BoatSystem.Application.Services
//{
//    public class BookingService : IBookingService
//    {
//        private readonly IBookingRepository _bookingRepository;
//        private readonly ICustomerRepository _customerRepository;
//        private readonly ITripRepository _tripRepository; // Add this line

//        public BookingService(IBookingRepository bookingRepository, ICustomerRepository customerRepository, ITripRepository tripRepository)
//        {
//            _bookingRepository = bookingRepository;
//            _customerRepository = customerRepository;
//            _tripRepository = tripRepository; // Add this line
//        }

//        public async Task<BoatBooking> GetBookingByIdAsync(int id)
//        {
//            return await _bookingRepository.GetByIdAsync(id);
//        }

//        public async Task CreateBookingAsync(BoatBooking booking)
//        {
//            await _bookingRepository.CreateAsync(booking);
//        }

//        public async Task<IEnumerable<BoatBooking>> GetBookingsByCustomerIdAsync(int customerId)
//        {
//            return await _bookingRepository.GetBookingsByCustomerIdAsync(customerId);
//        }

//        public async Task<IEnumerable<BoatBooking>> GetBookingsByTripIdAsync(int tripId)
//        {
//            return await _bookingRepository.GetBookingsByTripIdAsync(tripId);
//        }
//        public async Task<int?> GetCustomerIdByUserIdAsync(string userId)
//        {
//            var customer = await _customerRepository.GetCutomerByUserIdAsync(userId);
//            return customer?.Id;
//        }

//        public async Task<bool> IsTripExistsAsync(int tripId)
//        {
//            return await _tripRepository.ExistsAsync(tripId); // Make sure ExistsAsync is implemented in ITripRepository
//        }
//    }
//}
