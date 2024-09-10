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
        public DateTime StartedAt { get; set; } // إضافة تاريخ بدء الرحلة

    }



    public class CreateTripDto
    {
        public int BoatId { get; set; } // ID of the boat being used for the trip
        public string Name { get; set; } // Name of the trip
        public string Description { get; set; } // Description of the trip
        public decimal PricePerPerson { get; set; } // Price per person for the trip
        public int MaxPeople { get; set; } // Maximum number of people for the trip
        public DateTime CancellationDeadline { get; set; } // Maximum period for cancelling the trip
        public DateTime StartedAt { get; set; } // Start date and time of the trip
        public int OwnerId { get; set; } // ID of the owner creating the trip
    }

    public class UpdateTripDto
    {
        public int Id { get; set; } // ID of the trip to be updated
        public int BoatId { get; set; } // ID of the boat being used for the trip
        public string Name { get; set; } // Name of the trip
        public string Description { get; set; } // Description of the trip
        public decimal PricePerPerson { get; set; } // Price per person for the trip
        public int MaxPeople { get; set; } // Maximum number of people for the trip
        public DateTime CancellationDeadline { get; set; } // Maximum period for cancelling the trip
        public DateTime StartedAt { get; set; } // Start date and time of the trip
        public string Status { get; set; } // Status of the trip (e.g., available, cancelled)
    }
    public class TripDetailsDto
    {
        public int Id { get; set; } // ID of the trip
        public string Name { get; set; } // Name of the trip
        public string Description { get; set; } // Description of the trip
        public decimal PricePerPerson { get; set; } // Price per person for the trip
        public int MaxPeople { get; set; } // Maximum number of people for the trip
        public DateTime CancellationDeadline { get; set; } // Maximum period for cancelling the trip
        public DateTime StartedAt { get; set; } // Start date and time of the trip
        public int OwnerId { get; set; } // ID of the owner creating the trip
        public int BoatId { get; set; } // ID of the boat used for the trip
        public string Status { get; set; } // Status of the trip (e.g., available, cancelled)
        public DateTime CreatedAt { get; set; } // Date and time when the trip was created
        public DateTime UpdatedAt { get; set; } // Date and time when the trip was last updated
    }
    public class TripSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } // إضافة حالة الرحلة
        public DateTime StartedAt { get; set; } // إضافة تاريخ بدء الرحلة
    }


}