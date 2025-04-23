using EcommerceVidrieria.Application.Exceptions;
using EcommerceVidrieria.Application.Features.Auths.Users.Vms;
using EcommerceVidrieria.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Auths.Users.Commands.UpdateAdminStatusUser
{
    public class UpdateAdminStatusUserHandler : IRequestHandler<UpdateAdminStatusUserCommand, AuthResponse>
    {
        private readonly UserManager<User> _userManager;

        public UpdateAdminStatusUserHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AuthResponse> Handle(UpdateAdminStatusUserCommand request, CancellationToken cancellationToken)
        {
            var updateUser = await _userManager.FindByIdAsync(request.UserId!);

            if (updateUser == null)
            {
                throw new BadRequestException("El usuario no existe");
            }

            updateUser.IsActive = !updateUser.IsActive;

            var result = await _userManager.UpdateAsync(updateUser);

            if (!result.Succeeded)
            {
                throw new Exception("No se pudo cambiar el estado del usuario");
            }

            return new AuthResponse
            {
                Id = updateUser.Id,
                UserName = updateUser.UserName,
                Lastname = updateUser.LastName,
                Email = updateUser.Email,
                Roles = await _userManager.GetRolesAsync(updateUser)
            };
        }
    }
}
