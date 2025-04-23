using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Auths.Users.Commands.ResetPasswordByToken
{
    public class ResetPasswordByTokenCommand : IRequest<string>
    {
        public string? Password { get; set; }
        public string? OldPassword { get; set; }
    }
}
