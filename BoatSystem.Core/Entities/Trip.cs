using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Core.Entities
{
    public class Trip
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int BoatId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerPerson { get; set; }
        public int MaxPeople { get; set; }
        public DateTime CancellationDeadline { get; set; }
        public string Status { get; set; } 
        public DateTime StartedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("OwnerId")]
        public Owner Owner { get; set; }

        [ForeignKey("BoatId")]
        public Boat Boat { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
    }


}
