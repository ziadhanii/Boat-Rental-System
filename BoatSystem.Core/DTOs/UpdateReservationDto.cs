using BoatSystem.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace BoatSystem.Application.DTOs
{
    public class UpdateReservationDto
    {
        public DateTime? NewDate { get; set; }
        public int? NumPeople { get; set; }
        public decimal? TotalPrice { get; set; }
        public ReservationStatus? Status { get; set; }
    }
}
