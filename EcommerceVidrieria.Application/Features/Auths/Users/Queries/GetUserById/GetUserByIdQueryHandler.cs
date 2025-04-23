using CloudinaryDotNet.Core;
using EcommerceVidrieria.Application.Features.Auths.Users.Vms;
using EcommerceVidrieria.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Auths.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, AuthResponse>
    {
        private readonly UserManager<User> _userManager;

        public GetUserByIdQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<AuthResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId!);

            if (user == null)
            {
                throw new Exception("El usuario no existe");
            }

            return new AuthResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Lastname = user.LastName,
                Phone = user.PhoneNumber,
                Email = user.Email,
                Roles = await _userManager.GetRolesAsync(user)
            };
        }
    }
}
