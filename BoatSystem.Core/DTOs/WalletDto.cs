using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Core.DTOs
{
    public class WalletDto
    {
        public int OwnerId { get; set; }
        public decimal Balance { get; set; }
    }
    public class UpdateWalletDto
    {
        public int OwnerId { get; set; }
        public decimal Amount { get; set; }
    }
    public class CustomerWalletDto
    {
        public int CustomerId { get; set; } // استخدم CustomerId بدلاً من OwnerId
        public decimal Balance { get; set; }
    }
    public class UpdateCustomerWalletDto
    {
        public int CustomerId { get; set; } // استخدم CustomerId بدلاً من OwnerId
        public decimal Amount { get; set; }
    }
}
