using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Core.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumberOfPeople { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int TripId { get; set; }
        public Trip Trip { get; set; }
    }

}
