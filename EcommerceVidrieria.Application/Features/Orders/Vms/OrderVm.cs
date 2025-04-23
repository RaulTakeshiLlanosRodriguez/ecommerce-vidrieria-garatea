using EcommerceVidrieria.Application.Models.Order;
using EcommerceVidrieria.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Orders.Vms
{
    public class OrderVm
    {
        public int Id { get; set; }
        public List<OrderItemVm>? OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public decimal PriceDelivery { get; set; }
        public OrderStatus Status { get; set; }
        public string StatusLabel
        {
            get
            {
                switch(Status)
                {
                    case OrderStatus.Pending:
                        {
                            return OrderStatusLabel.PENDIENTE;
                        }
                    case OrderStatus.Completed:
                        {
                            return OrderStatusLabel.COMPLETADO;
                        }
                    case OrderStatus.Cancelled:
                        {
                            return OrderStatusLabel.ANULADO;
                        }
                    default: return OrderStatusLabel.PENDIENTE;
                }
            }
            set { }
        }
        public int Quantity
        {
            get
            {
                return OrderItems!.Sum(x => x.Quantity);
            }
            set { }
        }
    }
}
