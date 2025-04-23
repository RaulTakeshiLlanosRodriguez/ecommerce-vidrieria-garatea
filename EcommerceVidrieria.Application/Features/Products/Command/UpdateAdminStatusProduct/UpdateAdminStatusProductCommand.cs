using EcommerceVidrieria.Application.Features.Products.Vms;
using EcommerceVidrieria.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Products.Command.UpdateAdminStatusProduct
{
    public class UpdateAdminStatusProductCommand : IRequest<ProductVm>
    {
        public int ProductId { get; set; }

        public UpdateAdminStatusProductCommand(int productId)
        {
            ProductId = productId == 0 ? throw new ArgumentException(nameof(productId)) : productId;
        }
    }
}
