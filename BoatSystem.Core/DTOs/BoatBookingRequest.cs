using System;

public class BoatBookingRequest
{
    public int CustomerId { get; set; }
    public int BoatId { get; set; }
    public int? TripId { get; set; }
    public int DurationHours { get; set; }
    public decimal TotalPrice { get; set; }
    public int NumberOfPeople { get; set; }
    public DateTime CancellationDeadline { get; set; }
}
