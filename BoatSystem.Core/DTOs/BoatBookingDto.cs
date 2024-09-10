using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Core.DTOs
{
    //public class BookingDto
    //{
    //    public int BookingId { get; set; }
    //    public DateTime BookingDate { get; set; }
    //    public int NumberOfPeople { get; set; }
    //    public decimal TotalPrice { get; set; }
    //    public string Status { get; set; }
    //}
    public class BookingHistoryResponse
    {
        public List<BookingDto> Bookings { get; set; }
        public List<CancellationDto> Cancellations { get; set; }
    }

    public class BookingDto
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumberOfPeople { get; set; }
    }

    public class CancellationDto
    {
        public int Id { get; set; }
        public DateTime CancellationDate { get; set; }
        public decimal RefundAmount { get; set; }
    }


    public class BookingAdditionDto
    {
        public int AdditionId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }

}
