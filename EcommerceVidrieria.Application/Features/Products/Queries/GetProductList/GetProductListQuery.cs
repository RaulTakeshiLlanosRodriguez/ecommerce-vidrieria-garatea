using EcommerceVidrieria.Application.Features.Products.Vms;
using EcommerceVidrieria.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Products.Queries.GetProductList
{
    public class GetProductListQuery : IRequest<IReadOnlyList<ProductVm>>
    {
        public int? CategoryId { get; set; }
        public ProductStatus? Status { get; set; }
    }
}
