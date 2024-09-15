using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Core.Entities
{
    public class ReservationAddition
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int AdditionId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey("ReservationId")]
        public Reservation Reservation { get; set; }

        [ForeignKey("AdditionId")]
        public Addition Addition { get; set; }
    }
}
