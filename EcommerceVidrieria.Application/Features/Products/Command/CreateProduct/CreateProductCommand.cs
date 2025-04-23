using EcommerceVidrieria.Application.Features.Products.Vms;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Products.Command.CreateProduct
{
    public class CreateProductCommand : IRequest<ProductVm>
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public IReadOnlyList<IFormFile>? Photos { get; set; }
        public IReadOnlyList<CreateProductImageVm>? ImageUrls { get; set; }
    }
}
