using EcommerceVidrieria.Application.Features.Auths.Users.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Auths.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<AuthResponse>
    {
        public string? Password { get; set; }
        public string? Username { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
