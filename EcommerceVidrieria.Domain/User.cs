using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Domain
{
    public class User : IdentityUser
    {
        public string? LastName { get; set; }
        public bool IsActive { get; set; } = true;
        public IReadOnlyList<Order>? Orders { get; set; }
    }
}
