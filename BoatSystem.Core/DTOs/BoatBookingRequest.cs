using System;


//public class BoatBookingRequest
//{
//    public int CustomerId { get; set; }
//    public int BoatId { get; set; }
//    public int? TripId { get; set; }
//    public int DurationHours { get; set; }
//    public decimal TotalPrice { get; set; }
//    public int NumberOfPeople { get; set; }
//    public DateTime CancellationDeadline { get; set; }
//}

public class BoatBookingRequest
{
    public int CustomerId { get; set; }
    public int BoatId { get; set; }
    public DateTime BookingDate { get; set; }
    public int DurationHours { get; set; }
    public int NumberOfPeople { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CancellationDeadline { get; set; }
    public List<int> AdditionalServiceIds { get; set; } = new List<int>();
}


public class TripBookingRequest
{
    public int CustomerId { get; set; }
    public int TripId { get; set; }
    public DateTime BookingDate { get; set; }
    public int NumberOfPeople { get; set; }
    public DateTime CancellationDeadline { get; set; }
    public List<int> AdditionalServiceIds { get; set; } = new List<int>();
}
