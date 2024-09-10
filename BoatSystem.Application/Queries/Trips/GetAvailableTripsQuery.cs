using BoatSystem.Core.DTOs;
using BoatSystem.Core.Interfaces;
using BoatSystem.Core.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BoatSystem.Application.Queries.Trips
{
    public class GetAvailableTripsQuery : IRequest<IEnumerable<TripSummaryDto>> // استخدام TripDto بدلاً من TripSummaryDto
    {
    }

    public class GetAvailableTripsQueryHandler : IRequestHandler<GetAvailableTripsQuery, IEnumerable<TripSummaryDto>>
    {
        private readonly ITripRepository _tripRepository;

        public GetAvailableTripsQueryHandler(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<IEnumerable<TripSummaryDto>> Handle(GetAvailableTripsQuery request, CancellationToken cancellationToken)
        {
            var trips = await _tripRepository.GetAvailableTripsAsync();
            return trips.Select(trip => new TripSummaryDto
            {
                Id = trip.Id,
                Name = trip.Name,
                Price = trip.PricePerPerson,
                Status = trip.Status, // إضافة الحالة
                StartedAt = trip.StartedAt // إضافة تاريخ بدء الرحلة
            });
        }
    }
}
