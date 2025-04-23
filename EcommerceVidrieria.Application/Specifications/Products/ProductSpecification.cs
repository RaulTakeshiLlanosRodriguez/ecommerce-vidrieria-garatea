using EcommerceVidrieria.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Specifications.Products
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpecificationParams specificationParams) : base(
                x => (!specificationParams.CategoryId.HasValue || x.CategoryId == specificationParams.CategoryId)
                && (!specificationParams.Status.HasValue || x.Status == specificationParams.Status)
            )
        {
            AddInclude(p => p.Images!);
            AddInclude(p => p.Category!);
        }
    }
}
