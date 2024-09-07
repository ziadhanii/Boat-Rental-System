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
    public class UpdateReservationCommand : IRequest
    {
        public int ReservationId { get; set; }
        public DateTime NewDate { get; set; }
        // باقي الخصائص التي يمكن تعديلها
    }

    public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand>
    {
        private readonly IReservationRepository _reservationRepository;

        public UpdateReservationCommandHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId);
            if (reservation == null)
                throw new InvalidOperationException($"Reservation with ID {request.ReservationId} not found.");

            reservation.ReservationDate = request.NewDate; // تأكد من استخدام الخاصية الصحيحة
            await _reservationRepository.UpdateAsync(reservation);
        }
    }

}
