using BoatSystem.Core.DTOs;
using BoatSystem.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Application.Queries.Trips
{
    public class GetTripByIdQuery : IRequest<TripDetailsDto>
    {
        public int Id { get; set; }
    }

    public class GetTripsByOwnerIdQuery : IRequest<IEnumerable<TripSummaryDto>>
    {
        public int OwnerId { get; set; }
    }

    public class GetTripByIdHandler : IRequestHandler<GetTripByIdQuery, TripDetailsDto>
    {
        private readonly ITripRepository _tripRepository;

        public GetTripByIdHandler(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<TripDetailsDto> Handle(GetTripByIdQuery request, CancellationToken cancellationToken)
        {
            var trip = await _tripRepository.GetByIdAsync(request.Id);
            if (trip == null)
            {
                return null;
            }

            return new TripDetailsDto
            {
                Id = trip.Id,
                Name = trip.Name,
                Description = trip.Description,
                PricePerPerson = trip.PricePerPerson,
                MaxPeople = trip.MaxPeople,
                CancellationDeadline = trip.CancellationDeadline,
                StartedAt = trip.StartedAt,
                OwnerId = trip.OwnerId // Ensure this is set
            };
        }
    }


    public class GetTripsByOwnerIdQueryHandler : IRequestHandler<GetTripsByOwnerIdQuery, IEnumerable<TripSummaryDto>>
    {
        private readonly ITripRepository _tripRepository;

        public GetTripsByOwnerIdQueryHandler(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<IEnumerable<TripSummaryDto>> Handle(GetTripsByOwnerIdQuery request, CancellationToken cancellationToken)
        {
            var trips = await _tripRepository.GetByOwnerIdAsync(request.OwnerId);

            return trips.Select(trip => new TripSummaryDto
            {
                Id = trip.Id,
                Name = trip.Name,
                Price = trip.PricePerPerson
            }).ToList();
        }
    }
}
