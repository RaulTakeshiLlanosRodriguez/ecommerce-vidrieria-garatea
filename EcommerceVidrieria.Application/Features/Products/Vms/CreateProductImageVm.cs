using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Products.Vms
{
    public class CreateProductImageVm
    {
        public string? Url { get; set; }
        public int ProductId { get; set; }
        public string? PublicCode { get; set; }
    }
}
