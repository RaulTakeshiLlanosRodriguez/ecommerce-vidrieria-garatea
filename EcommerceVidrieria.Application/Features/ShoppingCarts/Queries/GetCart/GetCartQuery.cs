using EcommerceVidrieria.Application.Features.ShoppingCarts.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.ShoppingCarts.Queries.GetCart
{
    public class GetCartQuery : IRequest<ShoppingCartVm>
    {
        public Guid? ShoppingCartId { get; set; }

        public GetCartQuery(Guid? shoppingCartId)
        {
            ShoppingCartId = shoppingCartId ?? throw new ArgumentNullException(nameof(shoppingCartId));
        }
    }
}
