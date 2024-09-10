using BoatSystem.Core.DTOs;
using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using BoatSystem.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoatSystem.Application.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;
        private readonly IOwnerRepository _ownerRepository;

        public TripService(ITripRepository tripRepository, IOwnerRepository ownerRepository)
        {
            _tripRepository = tripRepository;
            _ownerRepository = ownerRepository;
        }

        public async Task<TripDetailsDto> AddTripAsync(CreateTripDto tripDto)
        {
            var trip = new Trip
            {
                BoatId = tripDto.BoatId,
                Name = tripDto.Name,
                Description = tripDto.Description,
                PricePerPerson = tripDto.PricePerPerson,
                MaxPeople = tripDto.MaxPeople,
                CancellationDeadline = tripDto.CancellationDeadline,
                OwnerId = tripDto.OwnerId,
                Status = "Active", // Default status
                StartedAt = tripDto.StartedAt,
                CreatedAt = DateTime.UtcNow, // Set CreatedAt to current time
                UpdatedAt = DateTime.UtcNow  // Set UpdatedAt to current time
            };

            var addedTrip = await _tripRepository.AddAsync(trip);

            return new TripDetailsDto
            {
                Id = addedTrip.Id,
                BoatId = addedTrip.BoatId,
                Name = addedTrip.Name,
                Description = addedTrip.Description,
                PricePerPerson = addedTrip.PricePerPerson,
                MaxPeople = addedTrip.MaxPeople,
                CancellationDeadline = addedTrip.CancellationDeadline,
                Status = addedTrip.Status,
                StartedAt = addedTrip.StartedAt,
                CreatedAt = addedTrip.CreatedAt,
                UpdatedAt = addedTrip.UpdatedAt
            };
        }

        public async Task<TripDetailsDto> UpdateTripAsync(UpdateTripDto tripDto)
        {
            var existingTrip = await _tripRepository.GetByIdAsync(tripDto.Id);

            if (existingTrip == null)
            {
                return null;
            }

            existingTrip.BoatId = tripDto.BoatId;
            existingTrip.Name = tripDto.Name;
            existingTrip.Description = tripDto.Description;
            existingTrip.PricePerPerson = tripDto.PricePerPerson;
            existingTrip.MaxPeople = tripDto.MaxPeople;
            existingTrip.CancellationDeadline = tripDto.CancellationDeadline;
            existingTrip.Status = tripDto.Status;
            existingTrip.StartedAt = tripDto.StartedAt;
            existingTrip.UpdatedAt = DateTime.UtcNow; // Update UpdatedAt to current time

            var updated = await _tripRepository.UpdateAsync(existingTrip);

            if (!updated)
            {
                return null;
            }

            return new TripDetailsDto
            {
                Id = existingTrip.Id,
                BoatId = existingTrip.BoatId,
                Name = existingTrip.Name,
                Description = existingTrip.Description,
                PricePerPerson = existingTrip.PricePerPerson,
                MaxPeople = existingTrip.MaxPeople,
                CancellationDeadline = existingTrip.CancellationDeadline,
                Status = existingTrip.Status,
                StartedAt = existingTrip.StartedAt,
                CreatedAt = existingTrip.CreatedAt,
                UpdatedAt = existingTrip.UpdatedAt
            };
        }

        public async Task<bool> DeleteTripAsync(int id, int ownerId)
        {
            var trip = await _tripRepository.GetByIdAsync(id);

            if (trip == null || trip.OwnerId != ownerId)
            {
                return false;
            }

            return await _tripRepository.DeleteAsync(id);
        }

        public async Task<TripDetailsDto> GetTripByIdAsync(int id)
        {
            var trip = await _tripRepository.GetByIdAsync(id);
            if (trip == null)
                return null;

            return new TripDetailsDto
            {
                Id = trip.Id,
                BoatId = trip.BoatId,
                Name = trip.Name,
                Description = trip.Description,
                PricePerPerson = trip.PricePerPerson,
                MaxPeople = trip.MaxPeople,
                CancellationDeadline = trip.CancellationDeadline,
                Status = trip.Status,
                StartedAt = trip.StartedAt,
                CreatedAt = trip.CreatedAt,
                UpdatedAt = trip.UpdatedAt
            };
        }

        public async Task<IEnumerable<TripSummaryDto>> GetTripsByOwnerIdAsync(int ownerId)
        {
            var trips = await _tripRepository.GetByOwnerIdAsync(ownerId);

            return trips.Select(trip => new TripSummaryDto
            {
                Id = trip.Id,
                Name = trip.Name,
                Price = trip.PricePerPerson // Assuming Price is the same as PricePerPerson
            }).ToList();
        }

        public async Task<int?> GetOwnerIdByUserIdAsync(string userId)
        {
            var owner = await _ownerRepository.GetOwnerByUserIdAsync(userId);
            return owner?.Id;
        }
    }
}
