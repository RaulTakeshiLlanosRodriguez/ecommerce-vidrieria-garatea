using EcommerceVidrieria.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Specifications.Products
{
    public class ProductSpecificationParams
    {
        public int? CategoryId { get; set; }
        public ProductStatus? Status { get; set; }
    }
}
