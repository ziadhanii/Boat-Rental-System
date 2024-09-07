using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsApproved { get; set; }
    }
}
