using EcommerceVidrieria.Application.Features.Orders.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<OrderVm>
    {
        public string? DeliveryMethod { get; set; }
        public string? Dni { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public decimal PriceDelivery { get; set; }
        public List<OrderItemVm>? OrderItems { get; set; }
    }
}
