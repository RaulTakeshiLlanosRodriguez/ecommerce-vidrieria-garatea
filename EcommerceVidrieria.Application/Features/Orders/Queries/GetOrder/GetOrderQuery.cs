using EcommerceVidrieria.Application.Features.Orders.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Orders.Queries.GetOrder
{
    public class GetOrderQuery : IRequest<IReadOnlyList<OrderVm>>
    {
        public int? Id { get; set; }
        public string? UserId { get; set; }
    }
}
