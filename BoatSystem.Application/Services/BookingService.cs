using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using BoatSystem.Core.Models;
using BoatSystem.Core.Repositories;

public interface IBookingService
{
    Task<Result> BookBoatAsync(BoatBookingRequest request);
    Task<Result> BookTripAsync(TripBookingRequest request);
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
        if (request == null) throw new ArgumentNullException(nameof(request));

        var boat = await _boatRepository.GetByIdAsync(request.BoatId);
        if (boat == null) return Result.Failure("Boat not found.");

        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
        if (customer == null) return Result.Failure("Customer not found.");

        var boatBooking = new BoatBooking
        {
            CustomerId = request.CustomerId,
            BoatId = request.BoatId,
            BookingDate = DateTime.UtcNow,
            DurationHours = request.DurationHours,
            TotalPrice = request.TotalPrice,
            NumberOfPeople = request.NumberOfPeople,
            CancellationDeadline = request.CancellationDeadline,
            Status = "Confirmed",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _boatBookingRepository.AddAsync(boatBooking);
        return Result.Success("Boat booking created successfully.");
    }

    public async Task<Result> BookTripAsync(TripBookingRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var trip = await _tripRepository.GetByIdAsync(request.TripId);
        if (trip == null) return Result.Failure("Trip not found.");

        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
        if (customer == null) return Result.Failure("Customer not found.");

        var boatBooking = new BoatBooking
        {
            CustomerId = request.CustomerId,
            TripId = request.TripId,
            BookingDate = DateTime.UtcNow,
            NumberOfPeople = request.NumberOfPeople,
            CancellationDeadline = request.CancellationDeadline,
            CanceledAt = null
        };

        await _boatBookingRepository.AddAsync(boatBooking);
        return Result.Success("Trip booking created successfully.");
    }
}
