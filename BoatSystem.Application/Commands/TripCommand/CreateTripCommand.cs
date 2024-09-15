using BoatSystem.Core.DTOs;
using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using BoatSystem.Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace BoatSystem.Application.Trips.Commands
{
    public class CreateTripCommand : IRequest<TripDetailsDto>
    {
        public int BoatId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerPerson { get; set; }
        public int MaxPeople { get; set; }
        public DateTime CancellationDeadline { get; set; }
        public int OwnerId { get; set; }
        public DateTime StartedAt { get; set; }
    }

    public class UpdateTripCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int BoatId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerPerson { get; set; }
        public int MaxPeople { get; set; }
        public DateTime CancellationDeadline { get; set; }
        public string Status { get; set; }
        public DateTime StartedAt { get; set; }
    }

    public class DeleteTripCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
    }

    public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, TripDetailsDto>
    {
        private readonly ITripRepository _tripRepository;

        public CreateTripCommandHandler(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<TripDetailsDto> Handle(CreateTripCommand request, CancellationToken cancellationToken)
        {
            var trip = new Trip
            {
                BoatId = request.BoatId,
                Name = request.Name,
                Description = request.Description,
                PricePerPerson = request.PricePerPerson,
                MaxPeople = request.MaxPeople,
                CancellationDeadline = request.CancellationDeadline,
                OwnerId = request.OwnerId,
                Status = "Active",
                StartedAt = request.StartedAt,
                CreatedAt = DateTime.UtcNow, 
                UpdatedAt = DateTime.UtcNow   
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
    }

    public class UpdateTripCommandHandler : IRequestHandler<UpdateTripCommand, bool>
    {
        private readonly ITripRepository _tripRepository;

        public UpdateTripCommandHandler(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<bool> Handle(UpdateTripCommand request, CancellationToken cancellationToken)
        {
            var existingTrip = await _tripRepository.GetByIdAsync(request.Id);

            if (existingTrip == null)
            {
                return false;
            }

            existingTrip.BoatId = request.BoatId;
            existingTrip.Name = request.Name;
            existingTrip.Description = request.Description;
            existingTrip.PricePerPerson = request.PricePerPerson;
            existingTrip.MaxPeople = request.MaxPeople;
            existingTrip.CancellationDeadline = request.CancellationDeadline;
            existingTrip.Status = request.Status;
            existingTrip.StartedAt = request.StartedAt;
            existingTrip.UpdatedAt = DateTime.UtcNow; 

            return await _tripRepository.UpdateAsync(existingTrip);
        }
    }

    public class DeleteTripCommandHandler : IRequestHandler<DeleteTripCommand, bool>
    {
        private readonly ITripRepository _tripRepository;

        public DeleteTripCommandHandler(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<bool> Handle(DeleteTripCommand request, CancellationToken cancellationToken)
        {
            var trip = await _tripRepository.GetByIdAsync(request.Id);

            if (trip == null || trip.OwnerId != request.OwnerId)
            {
                return false;
            }

            return await _tripRepository.DeleteAsync(request.Id);
        }
    }
}
