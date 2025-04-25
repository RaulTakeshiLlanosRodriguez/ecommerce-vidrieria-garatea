using EcommerceVidrieria.Application.Features.Orders.Vms;
using EcommerceVidrieria.Application.Features.Reports.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Reports.Queries.GetTopOrdersByDate
{
    public class GetTopOrdersByDateQuery : IRequest<ReportVm<List<OrderVm>>>
    {
    }
}
