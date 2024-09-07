using System.ComponentModel.DataAnnotations.Schema;

namespace BoatSystem.Core.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TripId { get; set; }
        public int BoatId { get; set; }
        public int NumPeople { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime ReservationDate { get; set; }
        public ReservationStatus Status { get; set; } // Changed from string to ReservationStatus
        public DateTime? CanceledAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [ForeignKey("TripId")]
        public Trip Trip { get; set; }

        [ForeignKey("BoatId")]
        public Boat Boat { get; set; }
        public virtual ICollection<ReservationAddition> ReservationAdditions { get; set; } = new HashSet<ReservationAddition>();
    }
}
