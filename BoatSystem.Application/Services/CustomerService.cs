using BoatSystem.Core.DTOs;
using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using BoatSystem.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoatSystem.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ITripRepository _tripRepository;
        private readonly IBoatRepository _boatRepository;
        private readonly IBookingRepository _bookingRepository;

        public CustomerService(
            ICustomerRepository customerRepository,
            IWalletRepository walletRepository,
            ITripRepository tripRepository,
            IBoatRepository boatRepository,
            IBookingRepository bookingRepository)
        {
            _customerRepository = customerRepository;
            _walletRepository = walletRepository;
            _tripRepository = tripRepository;
            _boatRepository = boatRepository;
            _bookingRepository = bookingRepository;
        }

        // Get Customer ID by User ID
        public async Task<int?> GetCustomerIdByUserIdAsync(string userId)
        {
            return await _customerRepository.GetCustomerIdByUserIdAsync(userId);
        }

        // Register a new Customer (commented out)
        // public async Task<bool> RegisterCustomerAsync(Customer customer)
        // {
        //     return await _customerRepository.AddCustomerAsync(customer);
        // }

        // Get Wallet Balance
        public async Task<decimal> GetWalletBalanceAsync(int customerId)
        {
            return await _walletRepository.GetBalanceAsync(customerId);
        }

        // Add funds to Wallet
        public async Task<bool> AddFundsToWalletAsync(int customerId, decimal amount)
        {
            return await _walletRepository.AddFundsAsync(customerId, amount);
        }

        // Browse available Trips
        public async Task<IEnumerable<Trip>> BrowseAvailableTripsAsync()
        {
            return await _tripRepository.GetAvailableTripsAsync();
        }

        // Browse available Boats
        public async Task<IEnumerable<BoatDto>> BrowseAvailableBoatsAsync()
        {
            return await _boatRepository.GetAvailableBoatsAsync();
        }

        // Book a Trip
        public async Task<BoatBooking> BookTripAsync(int customerId, int tripId, int participants, List<int> additionalServiceIds)
        {
            return await _bookingRepository.BookTripAsync(customerId, tripId, participants, additionalServiceIds);
        }

        // Book a Boat
        public async Task<BoatBooking> BookBoatAsync(int customerId, int boatId, List<int> serviceIds, string purpose)
        {
            return await _bookingRepository.BookBoatAsync(customerId, boatId, serviceIds, purpose);
        }

        // Calculate Total Cost of a booking
        public async Task<decimal> CalculateTotalCostAsync(int bookingId)
        {
            return await _bookingRepository.CalculateTotalCostAsync(bookingId);
        }

        // Cancel a Booking
        public async Task<bool> CancelBookingAsync(int bookingId)
        {
            return await _bookingRepository.CancelBookingAsync(bookingId);
        }

        // View Booking History
        public async Task<IEnumerable<BoatBooking>> GetBookingHistoryAsync(int customerId)
        {
            return await _bookingRepository.GetBookingHistoryAsync(customerId);
        }
    }
}
