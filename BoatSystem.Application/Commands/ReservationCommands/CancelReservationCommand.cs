using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Application.Commands.ReservationCommands
{
    public class CancelReservationCommand : IRequest
    {
        public int ReservationId { get; set; }
    }

    public class CancelReservationCommandHandler : IRequestHandler<CancelReservationCommand>
    {
        private readonly IReservationRepository _reservationRepository;

        public CancelReservationCommandHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task Handle(CancelReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId);
            if (reservation == null)
                throw new InvalidOperationException($"Reservation with ID {request.ReservationId} not found.");
            reservation.Status = ReservationStatus.Cancelled; // تأكد من تعريف ReservationStatus بشكل صحيح.
            await _reservationRepository.UpdateAsync(reservation);
        }
    }

}
