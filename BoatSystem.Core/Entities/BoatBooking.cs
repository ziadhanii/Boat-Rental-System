using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Core.Entities
{
    public class BoatBooking
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int BoatId { get; set; }
        public DateTime BookingDate { get; set; }
        public int DurationHours { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } // e.g., Pending, Confirmed, Canceled
        public DateTime? CanceledAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [ForeignKey("BoatId")]
        public Boat Boat { get; set; }
        public virtual ICollection<BookingAddition> BookingAdditions { get; set; } = new HashSet<BookingAddition>();
    }

}
