using EcommerceVidrieria.Application.Features.Orders.Vms;
using EcommerceVidrieria.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<OrderVm>
    {
        public int Orderid { get; set; }
        public OrderStatus Status {  get; set; }
    }
}
