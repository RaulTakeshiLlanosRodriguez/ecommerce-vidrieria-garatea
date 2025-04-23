using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Auths.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(p => p.Username).NotEmpty().WithMessage("El nombre no puede ser nulo");
            RuleFor(p => p.Lastname).NotEmpty().WithMessage("El apellido no puede ser nulo");
            RuleFor(p => p.Password).NotEmpty().WithMessage("La contraseña no puede ser nulo");
            RuleFor(p => p.Email).NotEmpty().WithMessage("El email no puede ser nulo");
        }
    }
}
