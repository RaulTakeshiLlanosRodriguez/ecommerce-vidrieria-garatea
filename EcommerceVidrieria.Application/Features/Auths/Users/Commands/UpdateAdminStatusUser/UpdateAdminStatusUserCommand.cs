using EcommerceVidrieria.Application.Features.Auths.Users.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Auths.Users.Commands.UpdateAdminStatusUser
{
    public class UpdateAdminStatusUserCommand : IRequest<AuthResponse>
    {
        public string? UserId { get; set; }

        public UpdateAdminStatusUserCommand(string? userId)
        {
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
        }
    }
}
