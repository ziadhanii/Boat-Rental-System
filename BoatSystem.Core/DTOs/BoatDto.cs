﻿namespace BoatSystem.Core.DTOs
{
    public class BoatDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public decimal ReservationPrice { get; set; }
        public int OwnerId { get; set; } 
    }

    public class BoatDetailsDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public decimal ReservationPrice { get; set; }
        public int OwnerId { get; set; } 
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class BoatSummaryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public decimal ReservationPrice { get; set; }
        public int OwnerId { get; set; } 
        public string Status { get; set; }
    }

    public class BoatApprovalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public decimal ReservationPrice { get; set; }
        public int OwnerId { get; set; } 
        public string Status { get; set; }
    }


}
