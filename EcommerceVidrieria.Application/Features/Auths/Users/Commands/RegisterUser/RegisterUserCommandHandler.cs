using CloudinaryDotNet.Core;
using EcommerceVidrieria.Application.Contracts.Identity;
using EcommerceVidrieria.Application.Exceptions;
using EcommerceVidrieria.Application.Features.Auths.Users.Vms;
using EcommerceVidrieria.Application.Models.Authorization;
using EcommerceVidrieria.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Auths.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;

        public RegisterUserCommandHandler(UserManager<User> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userExitsByEmail = await _userManager.FindByEmailAsync(request.Email!) == null ? false : true;

            if (userExitsByEmail)
            {
                throw new BadRequestException("El email del usuario ya existe");
            }

            var user = new User
            {
                LastName = request.Lastname,
                PhoneNumber = request.Phone,
                Email = request.Email,
                UserName = request.Username
            };

            var result = await _userManager.CreateAsync(user!, request.Password!);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Rol.GenericUser);
                var roles = await _userManager.GetRolesAsync(user);
                return new AuthResponse
                {
                    Id = user.Id,
                    UserName = user.PhoneNumber,
                    Lastname = user.LastName,
                    Phone = user.PhoneNumber,
                    Email = user.Email,
                    Token = _authService.CreateToken(user, roles),
                    Roles = roles
                };
            }

            throw new Exception("No se pudo registrar el usuario");
        }
    }
}
