using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using BoatSystem.Core.Models;
using BoatSystem.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Application.Services
{
    public interface IBookingService
    {
        Task<Result> BookBoatAsync(BoatBookingRequest request);
    }
    public class BookingService : IBookingService
    {
        private readonly IBoatBookingRepository _boatBookingRepository;
        private readonly IBoatRepository _boatRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITripRepository _tripRepository;

        public BookingService(
            IBoatBookingRepository boatBookingRepository,
            IBoatRepository boatRepository,
            ICustomerRepository customerRepository,
            ITripRepository tripRepository)
        {
            _boatBookingRepository = boatBookingRepository;
            _boatRepository = boatRepository;
            _customerRepository = customerRepository;
            _tripRepository = tripRepository;
        }

        public async Task<Result> BookBoatAsync(BoatBookingRequest request)
        {
            var boat = await _boatRepository.GetByIdAsync(request.BoatId);
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
            var trip = request.TripId.HasValue ? await _tripRepository.GetByIdAsync(request.TripId.Value) : null;

            if (boat == null)
            {
                return Result.Failure("Boat not found.");
            }

            if (customer == null)
            {
                return Result.Failure("Customer not found.");
            }

            var booking = new BoatBooking
            {
                CustomerId = request.CustomerId,
                BoatId = request.BoatId,
                TripId = request.TripId,
                BookingDate = DateTime.UtcNow,
                DurationHours = request.DurationHours,
                TotalPrice = request.TotalPrice,
                NumberOfPeople = request.NumberOfPeople,
                CancellationDeadline = request.CancellationDeadline
            };

            await _boatBookingRepository.AddAsync(booking);

            return Result.Success("Booking created successfully.");
        }
    }
}
