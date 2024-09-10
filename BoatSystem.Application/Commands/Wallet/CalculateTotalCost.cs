using BoatSystem.Core.Exceptions;
using BoatSystem.Core.Interfaces;
using BoatSystem.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoatSystem.Application.Commands.Wallet
{
    // ICostCalculatorService.cs
    public interface ICostCalculatorService
    {
        Task<decimal> CalculateTotalCostAsync(int tripId, int numberOfPeople, List<int> additionalServiceIds);
    }

    // CostCalculatorService.cs
    public class CostCalculatorService : ICostCalculatorService
    {
        private readonly ITripRepository _tripRepository;
        private readonly IBookingAdditionRepository _bookingAdditionRepository;

        public CostCalculatorService(
            ITripRepository tripRepository,
            IBookingAdditionRepository bookingAdditionRepository)
        {
            _tripRepository = tripRepository;
            _bookingAdditionRepository = bookingAdditionRepository;
        }

        public async Task<decimal> CalculateTotalCostAsync(int tripId, int numberOfPeople, List<int> additionalServiceIds)
        {
            // Retrieve the trip details
            var trip = await _tripRepository.GetByIdAsync(tripId);
            if (trip == null)
            {
                throw new NotFoundException("Trip not found");
            }

            // Calculate the base cost
            var totalPrice = trip.PricePerPerson * numberOfPeople;

            // If there are no additional services, return the base price
            if (additionalServiceIds == null || !additionalServiceIds.Any())
            {
                return totalPrice;
            }

            // Retrieve and add the cost of additional services
            var additionalServices = await _bookingAdditionRepository.GetByIdsAsync(additionalServiceIds);
            if (additionalServices == null || !additionalServices.Any())
            {
                // If no additional services are found, just return the base price
                return totalPrice;
            }

            totalPrice += additionalServices.Sum(service => service.TotalPrice);

            return totalPrice;
        }
    }
}
