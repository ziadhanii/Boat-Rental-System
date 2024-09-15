using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using BoatSystem.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Application.Commands
{
    public class CancelBookingCommand : IRequest<Result>
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CancellationDate { get; set; }
    }
    public class CancelBookingCommandHandler : IRequestHandler<CancelBookingCommand, Result>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBoatBookingRepository _boatBookingRepository;
        private readonly ICancellationRepository _cancellationRepository;

        public CancelBookingCommandHandler(
            IBookingRepository bookingRepository,
            IBoatBookingRepository boatBookingRepository,
            ICancellationRepository cancellationRepository)
        {
            _bookingRepository = bookingRepository;
            _boatBookingRepository = boatBookingRepository;
            _cancellationRepository = cancellationRepository;
        }

        public async Task<Result> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetByIdAsync(request.BookingId);
            var boatBooking = await _boatBookingRepository.GetByIdAsync(request.BookingId);

            if (booking == null && boatBooking == null)
            {
                return Result.Failure("Booking not found.");
            }

            var customerId = booking?.CustomerId ?? boatBooking?.CustomerId;
            if (customerId != request.CustomerId)
            {
                return Result.Failure("Customer ID does not match.");
            }

            DateTime cancellationDeadline;
            if (booking != null)
            {
                cancellationDeadline = booking.CancellationDeadline;
            }
            else
            {
                cancellationDeadline = boatBooking.CancellationDeadline;
            }

            if (request.CancellationDate > cancellationDeadline)
            {
                return Result.Failure("Cancellation deadline has passed.");
            }

            decimal refundAmount;
            if (booking != null)
            {
                refundAmount = CalculateRefundAmount(booking, request.CancellationDate);
            }
            else
            {
                refundAmount = CalculateRefundAmount(boatBooking, request.CancellationDate);
            }

            if (refundAmount <= 0)
            {
                return Result.Failure("Booking cannot be cancelled or refund not applicable.");
            }

            var cancellation = new Cancellation
            {
                CustomerId = request.CustomerId,
                BookingId = request.BookingId,
                CancellationDate = request.CancellationDate,
                RefundAmount = refundAmount,
                CreatedAt = DateTime.UtcNow
            };

            await _cancellationRepository.AddAsync(cancellation);

            if (booking != null)
            {
                booking.CanceledAt = request.CancellationDate; 
                await _bookingRepository.UpdateAsync(booking);
            }
            else if (boatBooking != null)
            {
                boatBooking.CanceledAt = request.CancellationDate; 
                await _boatBookingRepository.UpdateAsync(boatBooking);
            }

            return Result.Success("Booking cancelled successfully.");
        }

        private decimal CalculateRefundAmount(Booking booking, DateTime cancellationDate)
        {
            return 50.00m;  
        }

        private decimal CalculateRefundAmount(BoatBooking boatBooking, DateTime cancellationDate)
        {
            return 50.00m; 
        }
    }

}
