using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoatSystem.Core.Entities
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int TripId { get; set; }

        [Required]
        public int BoatId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Number of people must be greater than 0.")]
        public int? NumPeople { get; set; } // Nullable

        [Range(0, double.MaxValue, ErrorMessage = "Total price must be greater than or equal to 0.")]
        public decimal? TotalPrice { get; set; } // Nullable

        [Required]
        public DateTime ReservationDate { get; set; }

        [Required]
        public ReservationStatus Status { get; set; }

        public DateTime? CanceledAt { get; set; } // Nullable

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; }

        [ForeignKey(nameof(TripId))]
        public virtual Trip Trip { get; set; }

        [ForeignKey(nameof(BoatId))]
        public virtual Boat Boat { get; set; }

        public virtual ICollection<ReservationAddition> ReservationAdditions { get; set; } = new HashSet<ReservationAddition>();
    }
}




//using System.ComponentModel.DataAnnotations.Schema;

//namespace BoatSystem.Core.Entities
//{
//    public class Reservation
//    {
//        public int Id { get; set; }
//        public int CustomerId { get; set; }
//        public int TripId { get; set; }
//        public int BoatId { get; set; }
//        public int NumPeople { get; set; }
//        public decimal TotalPrice { get; set; }
//        public DateTime ReservationDate { get; set; }
//        public ReservationStatus Status { get; set; } // Changed from string to ReservationStatus
//        public DateTime? CanceledAt { get; set; }
//        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
//        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

//        // Navigation properties
//        [ForeignKey("CustomerId")]
//        public Customer Customer { get; set; }

//        [ForeignKey("TripId")]
//        public Trip Trip { get; set; }

//        [ForeignKey("BoatId")]
//        public Boat Boat { get; set; }
//        public virtual ICollection<ReservationAddition> ReservationAdditions { get; set; } = new HashSet<ReservationAddition>();
//    }
//}
