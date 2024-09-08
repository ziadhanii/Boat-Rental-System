using System.ComponentModel.DataAnnotations;

namespace BoatSystem.Core.Entities
{
    public enum ReservationStatus
    {
        Pending = 1,
        Confirmed = 2,
        Cancelled = 3  // تأكد من أن هذا موجود
    }

}
