using BoatSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoatSystem.Core.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string UserId { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal WalletBalance { get; set; } = 0.00M;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [NotMapped] 
        public string FullName => $"{FirstName} {LastName}";
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; } 

        public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
        public virtual ICollection<BoatBooking> BoatBookings { get; set; } = new HashSet<BoatBooking>();
        public virtual ICollection<Cancellation> Cancellations { get; set; } = new HashSet<Cancellation>();
        public virtual ICollection<TripBooking> TripBookings { get; set; } = new HashSet<TripBooking>(); 
    }
}
