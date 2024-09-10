//using BoatSystem.Application.Commands.Wallet;
//using BoatSystem.Core.DTOs;
//using BoatSystem.Core.Entities;
//using BoatSystem.Core.Exceptions;
//using BoatSystem.Core.Interfaces;
//using MediatR;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace BoatSystem.Application.Commands.BoatCommands
//{
//    public class BookBoatCommand : IRequest<BookingDto>
//    {
//        public int BoatId { get; set; }
//        public int? TripId { get; set; } // Nullable if booking for boat only
//        public int CustomerId { get; set; }
//        public DateTime BookingDate { get; set; }
//        public int DurationHours { get; set; }
//        public int NumberOfPeople { get; set; }
//        public List<int> AdditionalServiceIds { get; set; } // Additional services
//    }

//    public class BookBoatCommandHandler : IRequestHandler<BookBoatCommand, BookingDto>
//    {
//        private readonly IBoatRepository _boatRepository;
//        private readonly ICustomerRepository _customerRepository;
//        private readonly IBookingRepository _bookingRepository;
//        private readonly ICostCalculatorService _costCalculator;
//        private readonly IBookingAdditionRepository _bookingAdditionRepository;
//        private readonly ILogger<BookBoatCommandHandler> _logger;

//        public BookBoatCommandHandler(
//            IBoatRepository boatRepository,
//            ICustomerRepository customerRepository,
//            IBookingRepository bookingRepository,
//            ICostCalculatorService costCalculator,
//            IBookingAdditionRepository bookingAdditionRepository,
//            ILogger<BookBoatCommandHandler> logger)
//        {
//            _boatRepository = boatRepository;
//            _customerRepository = customerRepository;
//            _bookingRepository = bookingRepository;
//            _costCalculator = costCalculator;
//            _bookingAdditionRepository = bookingAdditionRepository;
//            _logger = logger;
//        }

//        public async Task<BookingDto> Handle(BookBoatCommand request, CancellationToken cancellationToken)
//        {
//            try
//            {
//                // Validate Boat
//                var boat = await _boatRepository.GetByIdAsync(request.BoatId);
//                if (boat == null || !boat.IsApproved || boat.Status != "Available")
//                {
//                    _logger.LogError($"Boat with ID {request.BoatId} not found or not available.");
//                    throw new NotFoundException("Boat not found or not available.");
//                }

//                // Validate Customer
//                var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
//                if (customer == null)
//                {
//                    _logger.LogError($"Customer with ID {request.CustomerId} not found.");
//                    throw new NotFoundException("Customer not found.");
//                }

//                // Calculate total cost
//                var totalPrice = await _costCalculator.CalculateTotalCostAsync(
//                    request.TripId ?? request.BoatId,  // Use BoatId if TripId is not provided
//                    request.NumberOfPeople,
//                    request.AdditionalServiceIds ?? new List<int>()); // Ensure no null is passed

//                // Create and save booking
//                var booking = new BoatBooking
//                {
//                    CustomerId = request.CustomerId,
//                    BoatId = request.BoatId,
//                    TripId = request.TripId,
//                    BookingDate = request.BookingDate,
//                    DurationHours = request.DurationHours,
//                    NumberOfPeople = request.NumberOfPeople,
//                    TotalPrice = totalPrice,
//                    Status = "Booked"
//                };

//                await _bookingRepository.AddAsync(booking);

//                // Add booking additions
//                if (request.AdditionalServiceIds != null && request.AdditionalServiceIds.Any())
//                {
//                    foreach (var serviceId in request.AdditionalServiceIds)
//                    {
//                        var service = await _bookingAdditionRepository.GetByIdsAsync(new List<int> { serviceId });
//                        var servicePrice = service.FirstOrDefault()?.TotalPrice ?? 0m;

//                        var bookingAddition = new BookingAddition
//                        {
//                            BookingId = booking.Id,
//                            AdditionId = serviceId,
//                            Quantity = 1, // Adjust quantity as necessary
//                            TotalPrice = servicePrice
//                        };

//                        await _bookingAdditionRepository.AddAsync(bookingAddition);
//                    }
//                }

//                // Map to DTO
//                var bookingDto = new BookingDto
//                {
//                    BookingId = booking.Id,
//                    BookingDate = booking.BookingDate,
//                    NumberOfPeople = booking.NumberOfPeople,
//                    TotalPrice = booking.TotalPrice,
//                    Status = booking.Status
//                };

//                return bookingDto;
//            }
//            catch (NotFoundException ex)
//            {
//                _logger.LogError(ex, "Not found error occurred.");
//                throw;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "An unexpected error occurred.");
//                throw;
//            }
//        }
//    }
//}
