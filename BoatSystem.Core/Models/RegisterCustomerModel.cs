using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Core.Models
{
namespace BoatSystem.Application.Models
{
        public class RegisterCustomerModel
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public decimal? InitialWalletBalance { get; set; } // Nullable للسماح بقيمة مبدئية غير محددة
        }
    }


}
