using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Auths.Users.Vms
{
    public class AuthResponse
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public ICollection<string>? Roles { get; set; }
    }
}
