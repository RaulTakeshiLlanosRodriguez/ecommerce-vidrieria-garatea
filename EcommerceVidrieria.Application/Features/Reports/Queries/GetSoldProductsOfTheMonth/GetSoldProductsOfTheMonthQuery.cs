using EcommerceVidrieria.Application.Features.Products.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Reports.Queries.GetSoldProductsOfTheMonth
{
    public class GetSoldProductsOfTheMonthQuery : IRequest<List<ProductVm>>
    {
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
