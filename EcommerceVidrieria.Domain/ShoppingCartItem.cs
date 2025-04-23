using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Domain
{
    public class ShoppingCartItem : BaseDomainModel
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid? ShoppingCartMasterId { get; set; }
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public virtual ShoppingCart? ShoppingCart { get; set; }
    }
}
