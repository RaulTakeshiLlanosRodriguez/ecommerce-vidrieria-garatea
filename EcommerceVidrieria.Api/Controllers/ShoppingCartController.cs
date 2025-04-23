using EcommerceVidrieria.Application.Features.ShoppingCarts.Commands.DeleteShoppingCartItem;
using EcommerceVidrieria.Application.Features.ShoppingCarts.Commands.UpdateShoppingCart;
using EcommerceVidrieria.Application.Features.ShoppingCarts.Queries.GetCart;
using EcommerceVidrieria.Application.Features.ShoppingCarts.Vms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EcommerceVidrieria.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private IMediator _mediator;

        public ShoppingCartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("", Name = "UpdateShoppingCart")]
        [ProducesResponseType(typeof(ShoppingCartVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartVm>> UpdateShoppingCart(UpdateShoppingCartCommand request)
        {
            return await _mediator.Send(request);
        }


        [HttpDelete("{id}", Name = "DeleteShoppingCart")]
        [ProducesResponseType(typeof(ShoppingCartVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartVm>> DeleteShoppingCart(Guid id)
        {
            return await _mediator.Send(new DeleteShoppingCartItemCommand()
            {
                ShoppingCartId = id
            });
        }

        [HttpGet("", Name = "GetShoppingCart")]
        [ProducesResponseType(typeof(ShoppingCartVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartVm>> GetShoppingCart()
        {
            var query = new GetCartQuery(Guid.NewGuid());
            return await _mediator.Send(query);
        }
    }
}
