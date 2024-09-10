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
//using Microsoft.EntityFrameworkCore;
//using BoatSystem.Core.Repositories;

//namespace BoatSystem.Application.Commands.BookTripCommand
//{
//    public class BookTripCommand : IRequest<BookingDto>
//    {
//        public int TripId { get; set; }
//        public int CustomerId { get; set; }
//        public int NumberOfPeople { get; set; }
//        public int? BoatId { get; set; } // Make this nullable if not always required
//        public decimal TotalPrice { get; set; }
//        public List<int> AdditionalServiceIds { get; set; }
//    }


//    public class BookTripHandler : IRequestHandler<BookTripCommand, BookingDto>
//    {
//        private readonly IBookingRepository _bookingRepository;
//        private readonly ITripRepository _tripRepository;
//        private readonly ICustomerRepository _customerRepository;
//        private readonly IBoatRepository _boatRepository; // Add this
//        private readonly IBookingAdditionRepository _bookingAdditionRepository;
//        private readonly ILogger<BookTripHandler> _logger;

//        public BookTripHandler(
//            IBookingRepository bookingRepository,
//            ITripRepository tripRepository,
//            ICustomerRepository customerRepository,
//            IBoatRepository boatRepository, // Add this
//            IBookingAdditionRepository bookingAdditionRepository,
//            ILogger<BookTripHandler> logger)
//        {
//            _bookingRepository = bookingRepository;
//            _tripRepository = tripRepository;
//            _customerRepository = customerRepository;
//            _boatRepository = boatRepository; // Initialize this
//            _bookingAdditionRepository = bookingAdditionRepository;
//            _logger = logger;
//        }

//        public async Task<BookingDto> Handle(BookTripCommand request, CancellationToken cancellationToken)
//        {
//            try
//            {
//                // Validate TripId
//                var trip = await _tripRepository.GetByIdAsync(request.TripId);
//                if (trip == null)
//                {
//                    _logger.LogError($"Trip with ID {request.TripId} not found.");
//                    throw new NotFoundException("Trip not found");
//                }

//                // Validate BoatId (if applicable)
//                if (request.BoatId.HasValue)
//                {
//                    var boat = await _boatRepository.GetByIdAsync(request.BoatId.Value);
//                    if (boat == null)
//                    {
//                        _logger.LogError($"Boat with ID {request.BoatId} not found.");
//                        throw new NotFoundException("Boat not found");
//                    }
//                }

//                // Create the booking
//                var booking = new BoatBooking
//                {
//                    CustomerId = request.CustomerId,
//                    TripId = request.TripId,
//                    BoatId = request.BoatId ?? 0,  // Use 0 if BoatId is not provided
//                    BookingDate = DateTime.UtcNow,
//                    DurationHours = trip.DurationHours,
//                    TotalPrice = request.TotalPrice,
//                    Status = "Booked",
//                    NumberOfPeople = request.NumberOfPeople
//                };

//                await _bookingRepository.AddAsync(booking);

//                // Return DTO
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
//            catch (DbUpdateException dbEx)
//            {
//                _logger.LogError(dbEx, "Database update error occurred.");
//                throw new Exception("A database error occurred.", dbEx);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "An unexpected error occurred.");
//                throw;
//            }
//        }
//    }
//}
