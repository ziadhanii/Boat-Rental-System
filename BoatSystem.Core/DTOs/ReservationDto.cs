using BoatSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Application.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string TripName { get; set; }
        public string BoatName { get; set; }
        public int? NumPeople { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Status { get; set; } 
        public DateTime? CanceledAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
