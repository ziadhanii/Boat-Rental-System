using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Core.DTOs
{
    public class TripDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerPerson { get; set; }
        public int MaxPeople { get; set; }
        public string Status { get; set; }
        public DateTime StartedAt { get; set; }
    }

    public class CreateTripDto
    {
        public int BoatId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerPerson { get; set; }
        public int MaxPeople { get; set; }
        public DateTime CancellationDeadline { get; set; }
        public DateTime StartedAt { get; set; }
        public int OwnerId { get; set; }
    }

    public class UpdateTripDto
    {
        public int Id { get; set; }
        public int BoatId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerPerson { get; set; }
        public int MaxPeople { get; set; }
        public DateTime CancellationDeadline { get; set; }
        public DateTime StartedAt { get; set; }
        public string Status { get; set; }
    }

    public class TripDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerPerson { get; set; }
        public int MaxPeople { get; set; }
        public DateTime CancellationDeadline { get; set; }
        public DateTime StartedAt { get; set; }
        public int OwnerId { get; set; }
        public int BoatId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class TripSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public DateTime StartedAt { get; set; }
    }
}
