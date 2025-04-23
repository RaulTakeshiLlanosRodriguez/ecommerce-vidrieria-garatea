using EcommerceVidrieria.Application.Features.Products.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery: IRequest<ProductVm>
    {
        public int ProductId { get; set; }

        public GetProductByIdQuery(int productId)
        {
            ProductId = productId == 0 ? throw new ArgumentNullException(nameof(productId)) : productId;
        }
    }
}
