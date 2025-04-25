using EcommerceVidrieria.Application.Features.Reports.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Reports.Queries.GetSalesByMonth
{
    public class GetSalesByMonthQuery : IRequest<List<SalesByMonthVm>>
    {
        public int Year { get; set; }
    }
}
