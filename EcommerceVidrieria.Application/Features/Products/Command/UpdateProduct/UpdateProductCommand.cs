using EcommerceVidrieria.Application.Features.Products.Vms;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Products.Command.UpdateProduct
{
    public class UpdateProductCommand : IRequest<ProductVm>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CateogoryId { get; set; }
        public IReadOnlyList<IFormFile>? Photos { get; set; }
        public IReadOnlyList<CreateProductImageVm>? ImageUrls { get; set; }
    }
}
