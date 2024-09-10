using BoatSystem.Application.DTOs;
using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BoatSystem.Application.Queries
{
    public class OwnerReservationsQuery : IRequest<IEnumerable<ReservationDto>>
    {
        public int OwnerId { get; set; }
    }

    public class OwnerReservationsQueryHandler : IRequestHandler<OwnerReservationsQuery, IEnumerable<ReservationDto>>
    {
        private readonly IReservationRepository _reservationRepository;

        public OwnerReservationsQueryHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<IEnumerable<ReservationDto>> Handle(OwnerReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _reservationRepository.GetAllIncludingDetailsAsync();

            // Filter reservations based on the ownerId
            var ownerReservations = reservations.Where(r => r.Boat.OwnerId == request.OwnerId);

            return ownerReservations.Select(r => new ReservationDto
            {
                Id = r.Id,
                CustomerName = r.Customer.FullName, // Assuming Customer has a FullName property
                TripName = r.Trip.Name, // Assuming Trip has a Name property
                BoatName = r.Boat.Name, // Assuming Boat has a Name property
                NumPeople = r.NumPeople,
                TotalPrice = r.TotalPrice,
                ReservationDate = r.ReservationDate,
                Status = r.Status.ToString(),
                CanceledAt = r.CanceledAt,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            });
        }
    }
}
