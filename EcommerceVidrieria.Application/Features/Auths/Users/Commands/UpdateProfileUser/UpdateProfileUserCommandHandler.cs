using CloudinaryDotNet.Core;
using EcommerceVidrieria.Application.Contracts.Identity;
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

namespace EcommerceVidrieria.Application.Features.Auths.Users.Commands.UpdateProfileUser
{
    public class UpdateProfileUserCommandHandler : IRequestHandler<UpdateProfileUserCommand, AuthResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;

        public UpdateProfileUserCommandHandler(UserManager<User> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(UpdateProfileUserCommand request, CancellationToken cancellationToken)
        {
            var updateUser = await _userManager.FindByIdAsync(_authService.GetSessionUser());

            if (updateUser == null)
            {
                throw new BadRequestException("El usuario no existe");
            }

            updateUser.UserName = request.Username;
            updateUser.LastName = request.Lastname;
            updateUser.PhoneNumber = request.Phone;
            updateUser.Email = request.Email;

            var result = await _userManager.UpdateAsync(updateUser);

            if (!result.Succeeded)
            {
                throw new Exception("No se puso actualizar el usuario");
            }

            var userById = await _userManager.FindByEmailAsync(request.Email!);
            var roles = await _userManager.GetRolesAsync(userById!);

            return new AuthResponse
            {
                Id = userById!.Id,
                UserName = userById.UserName,
                Lastname = userById.LastName,
                Phone = userById.PhoneNumber,
                Email = userById.Email,
                Token = _authService.CreateToken(userById, roles),
                Roles = roles
            };
        }
    }
}
