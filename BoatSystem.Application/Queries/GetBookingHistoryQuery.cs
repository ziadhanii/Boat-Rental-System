using BoatSystem.Core.DTOs;
using BoatSystem.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Application.Queries
{
    public class GetBookingHistoryQuery : IRequest<BookingHistoryResponse>
    {
        public int CustomerId { get; set; }

        public GetBookingHistoryQuery(int customerId)
        {
            CustomerId = customerId;
        }
    }


    public class GetBookingHistoryQueryHandler : IRequestHandler<GetBookingHistoryQuery, BookingHistoryResponse>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICancellationRepository _cancellationRepository;

        public GetBookingHistoryQueryHandler(IBookingRepository bookingRepository, ICancellationRepository cancellationRepository)
        {
            _bookingRepository = bookingRepository;
            _cancellationRepository = cancellationRepository;
        }

        public async Task<BookingHistoryResponse> Handle(GetBookingHistoryQuery request, CancellationToken cancellationToken)
        {
            var bookings = await _bookingRepository.GetBookingsByCustomerIdAsync(request.CustomerId);
            var cancellations = await _cancellationRepository.GetCancellationsByCustomerIdAsync(request.CustomerId);

            return new BookingHistoryResponse
            {
                Bookings = bookings.Select(b => new BookingDto
                {
                    BookingId = b.BookingId,
                    BookingDate = b.BookingDate,
                    NumberOfPeople = b.NumberOfPeople
                }).ToList(),
                Cancellations = cancellations.Select(c => new CancellationDto
                {
                    Id = c.Id,
                    CancellationDate = c.CancellationDate,
                    RefundAmount = c.RefundAmount
                }).ToList()
            };
        }
    }
}
