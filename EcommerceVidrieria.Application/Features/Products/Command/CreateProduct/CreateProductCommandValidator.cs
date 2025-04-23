using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Products.Command.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre no puede estar en blanco")
                .MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres");

            RuleFor(p => p.Stock)
                .NotEmpty().WithMessage("el stock no puede ser nulo");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("el precio no puede ser nulo");
        }
    }
}
