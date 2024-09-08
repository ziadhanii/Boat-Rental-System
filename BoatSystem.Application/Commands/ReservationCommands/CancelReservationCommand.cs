using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging; // استخدم هذه المساحة بدلاً من Serilog

namespace BoatSystem.Application.Commands.ReservationCommands
{
    public class CancelReservationCommand : IRequest
    {
        public int ReservationId { get; set; }
    }

    public class CancelReservationCommandHandler : IRequestHandler<CancelReservationCommand>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ILogger<CancelReservationCommandHandler> _logger;

        public CancelReservationCommandHandler(IReservationRepository reservationRepository, ILogger<CancelReservationCommandHandler> logger)
        {
            _reservationRepository = reservationRepository;
            _logger = logger;
        }

        public async Task Handle(CancelReservationCommand request, CancellationToken cancellationToken)
        {
            // Attempt to retrieve the reservation by ID
            var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId);

            if (reservation == null)
            {
                _logger.LogError("Reservation with ID {ReservationId} not found.", request.ReservationId);
                throw new InvalidOperationException($"Reservation with ID {request.ReservationId} not found.");
            }

            // Check if the reservation is already cancelled
            if (reservation.Status == ReservationStatus.Cancelled)
            {
                _logger.LogError("Attempted to cancel already cancelled reservation with ID {ReservationId}.", request.ReservationId);
                throw new InvalidOperationException($"Reservation with ID {request.ReservationId} is already cancelled.");
            }

            // Mark the reservation as cancelled
            reservation.Status = ReservationStatus.Cancelled;
            reservation.CanceledAt = DateTime.UtcNow;

            // Update the reservation in the repository
            await _reservationRepository.UpdateAsync(reservation);

            _logger.LogInformation("Reservation with ID {ReservationId} has been successfully cancelled.", request.ReservationId);
        }
    }
}
