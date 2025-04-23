using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Domain
{
    public class ShoppingCart : BaseDomainModel
    {
        public Guid? ShoppingCartMasterId { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
        public virtual ICollection<ShoppingCartItem>? ShoppingCartItems { get; set; }
    }
}
