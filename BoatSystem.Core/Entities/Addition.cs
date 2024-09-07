using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Core.Entities
{
    public class Addition
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Owner Owner { get; set; }
        public virtual ICollection<ReservationAddition> ReservationAdditions { get; set; } = new HashSet<ReservationAddition>();
        public virtual ICollection<BookingAddition> BookingAdditions { get; set; } = new HashSet<BookingAddition>();
    }
}
