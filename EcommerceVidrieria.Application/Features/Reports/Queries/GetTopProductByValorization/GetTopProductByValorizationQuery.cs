using EcommerceVidrieria.Application.Features.Products.Vms;
using EcommerceVidrieria.Application.Features.Reports.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Reports.Queries.GetTopProductByValorization
{
    public class GetTopProductByValorizationQuery : IRequest<ReportVm<List<ProductVm>>>
    {
    }
}
