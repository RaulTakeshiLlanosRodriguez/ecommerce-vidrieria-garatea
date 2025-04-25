using EcommerceVidrieria.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Specifications.Orders
{
    public class OrderSpecification : BaseSpecification<Order>
    {
        public OrderSpecification(OrderSpecificationParams specificationParams) : base(
                x => (!specificationParams.Id.HasValue || specificationParams.Id == x.Id)
                && (string.IsNullOrEmpty(specificationParams.UserId) || x.UserId == specificationParams.UserId)
            )
        {
            AddInclude(p => p.OrderItems!);
            AddInclude(p => p.City!);
        }
    }
}
