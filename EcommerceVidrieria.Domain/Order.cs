using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Domain
{
    public class Order : BaseDomainModel
    {
        public IReadOnlyList<OrderItem>? OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal TotalOrder { get; set; }
        public decimal PriceDelivery { get; set; }
        public string? DeliveryMethod { get; set; }
        public string? Dni { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int? CityId { get; set; }
        public City? City { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
