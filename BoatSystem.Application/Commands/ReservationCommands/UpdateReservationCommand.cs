using BoatSystem.Application.DTOs;
using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BoatSystem.Application.Commands.ReservationCommands
{
    public class UpdateReservationCommand : IRequest<bool>
    {
        public int ReservationId { get; set; }
        public UpdateReservationDto ReservationDto { get; set; }
    }

    public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, bool>
    {
        private readonly IReservationRepository _reservationRepository;

        public UpdateReservationCommandHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<bool> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId);
            if (reservation == null)
                throw new InvalidOperationException($"Reservation with ID {request.ReservationId} not found.");

            if (request.ReservationDto.NewDate.HasValue)
            {
                reservation.ReservationDate = request.ReservationDto.NewDate.Value;
            }

            if (request.ReservationDto.NumPeople.HasValue)
            {
                reservation.NumPeople = request.ReservationDto.NumPeople.Value;
            }

            if (request.ReservationDto.TotalPrice.HasValue)
            {
                reservation.TotalPrice = request.ReservationDto.TotalPrice.Value;
            }

            if (request.ReservationDto.Status.HasValue)
            {
                reservation.Status = request.ReservationDto.Status.Value;
            }

            reservation.UpdatedAt = DateTime.UtcNow;

            await _reservationRepository.UpdateAsync(reservation);

            return true; // Return true to indicate success
        }
    }
}
