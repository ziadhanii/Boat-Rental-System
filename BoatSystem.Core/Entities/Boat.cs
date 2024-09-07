using System.ComponentModel.DataAnnotations.Schema;

namespace BoatSystem.Core.Entities
{
    public class Boat
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public bool IsApproved { get; set; }

        public decimal ReservationPrice { get; set; }
        public string Status { get; set; } // e.g., Pending, Approved, Rejected
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        [ForeignKey("OwnerId")]
        public Owner Owner { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
        public virtual ICollection<Trip> Trips { get; set; } = new HashSet<Trip>();
        public virtual ICollection<BoatBooking> BoatBookings { get; set; } = new HashSet<BoatBooking>();

    }

}
