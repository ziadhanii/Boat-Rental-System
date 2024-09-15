using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Core.Entities
{
    public class Cancellation
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int? ReservationId { get; set; }
        public int? BookingId { get; set; }
        public DateTime CancellationDate { get; set; }
        public decimal RefundAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [ForeignKey("ReservationId")]
        public Reservation Reservation { get; set; }

        [ForeignKey("BookingId")]
        public BoatBooking BoatBooking { get; set; }

    }
}
