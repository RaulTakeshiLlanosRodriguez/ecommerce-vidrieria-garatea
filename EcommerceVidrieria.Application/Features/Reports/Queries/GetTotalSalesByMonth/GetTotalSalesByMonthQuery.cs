using EcommerceVidrieria.Application.Features.Reports.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Reports.Queries.GetTotalSalesByMonth
{
    public class GetTotalSalesByMonthQuery : IRequest<GetTotalSalesByMonthVm>
    {
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
