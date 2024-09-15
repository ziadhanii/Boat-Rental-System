using BoatSystem.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoatSystem.Core.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public string UserId { get; set; } 
        public bool IsVerified { get; set; }
        public string BusinessName { get; set; }
        public string Address { get; set; }
        public decimal WalletBalance { get; set; } = 0.00M;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsApproved { get; set; } 

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Boat> Boats { get; set; } = new HashSet<Boat>();
        public virtual ICollection<Trip> Trips { get; set; } = new HashSet<Trip>();
        public virtual ICollection<Addition> Additions { get; set; } = new HashSet<Addition>();
    }
}
