using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.ShoppingCarts.Vms
{
    public class ShoppingCartVm
    {
        public string? ShoppingCartId { get; set; }
        public string? UserId { get; set; }
        public List<ShoppingCartItemVm>? ShoppingCartItems { get; set; }

        public int Quantity
        {
            get
            {
                return ShoppingCartItems!.Sum(x => x.Quantity);
            }
            set { }
        }

        public decimal Total
        {
            get
            {
                return Math.Round(
                        (ShoppingCartItems!.Sum(x => x.Price * x.Quantity)), 2);
            }
            set { }
        }
    }
}
