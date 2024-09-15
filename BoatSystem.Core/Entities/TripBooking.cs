using System;

namespace BoatSystem.Core.Entities
{
    public class TripBooking
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TripId { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumberOfPeople { get; set; }
        public DateTime CancellationDeadline { get; set; }
        public DateTime? CanceledAt { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TripBookingStatus Status { get; set; } 

        public Customer Customer { get; set; }
        public Trip Trip { get; set; }
    }
}
