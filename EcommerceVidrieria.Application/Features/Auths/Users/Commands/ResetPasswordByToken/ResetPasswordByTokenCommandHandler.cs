using EcommerceVidrieria.Application.Contracts.Identity;
using EcommerceVidrieria.Application.Exceptions;
using EcommerceVidrieria.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Auths.Users.Commands.ResetPasswordByToken
{
    public class ResetPasswordByTokenCommandHandler : IRequestHandler<ResetPasswordByTokenCommand, string>
    {

        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;

        public ResetPasswordByTokenCommandHandler(UserManager<User> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        public async Task<string> Handle(ResetPasswordByTokenCommand request, CancellationToken cancellationToken)
        {
            var updateUsuario = await _userManager.FindByIdAsync(_authService.GetSessionUser());
            if (updateUsuario == null)
            {
                throw new BadRequestException("El usuario no existe");
            }

            var resultValidatePassword = _userManager.PasswordHasher
                .VerifyHashedPassword(updateUsuario, updateUsuario.PasswordHash!, request.OldPassword!);

            if (!(resultValidatePassword == PasswordVerificationResult.Success))
            {
                throw new BadRequestException("El actual password ingresado es incorrecto");
            }

            var hashedNewPassoword = _userManager.PasswordHasher.HashPassword(updateUsuario, request.Password!);
            updateUsuario.PasswordHash = hashedNewPassoword;

            var result = await _userManager.UpdateAsync(updateUsuario);

            if (!result.Succeeded)
            {
                throw new Exception("No se puso resetear el password");
            }

            return "Actualizacion de contraseña correcta";
        }
    }
}
