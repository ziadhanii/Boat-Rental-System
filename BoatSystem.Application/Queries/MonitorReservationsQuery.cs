﻿using BoatSystem.Application.DTOs;
using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BoatSystem.Application.Queries
{
    public class MonitorReservationsQuery : IRequest<IEnumerable<ReservationDto>>
    {
    }

    public class MonitorReservationsQueryHandler : IRequestHandler<MonitorReservationsQuery, IEnumerable<ReservationDto>>
    {
        private readonly IReservationRepository _reservationRepository;

        public MonitorReservationsQueryHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<IEnumerable<ReservationDto>> Handle(MonitorReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _reservationRepository.GetAllIncludingDetailsAsync();
            return reservations.Select(r => new ReservationDto
            {
                Id = r.Id,
                CustomerName = r.Customer.FullName,
                TripName = r.Trip.Name, 
                BoatName = r.Boat.Name, 
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

