using CloudinaryDotNet.Actions;
using EcommerceVidrieria.Application.Contracts.Identity;
using EcommerceVidrieria.Application.Features.Orders.Commands.CreateOrder;
using EcommerceVidrieria.Application.Features.Orders.Commands.UpdateOrder;
using EcommerceVidrieria.Application.Features.Orders.Queries.GetOrder;
using EcommerceVidrieria.Application.Features.Orders.Vms;
using EcommerceVidrieria.Application.Models.Authorization;
using EcommerceVidrieria.Infrastructure.Service.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EcommerceVidrieria.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IMediator _mediator;
        private readonly IAuthService _authService;

        public OrderController(IMediator mediator, IAuthService authService)
        {
            _mediator = mediator;
            _authService = authService;
        }

        [HttpPost(Name = "CreateOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderVm>> CreateOrder([FromBody] CreateOrderCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("user", Name = "PaginationOrderByUsername")]
        [ProducesResponseType(typeof(IReadOnlyList<OrderVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<OrderVm>>> PaginationOrderByUsername
            ([FromQuery] GetOrderQuery paginationOrdersParams)
        {
            paginationOrdersParams.UserId = _authService.GetSessionUser();
            var pagination = await _mediator.Send(paginationOrdersParams);
            return Ok(pagination);
        }

        [Authorize(Roles = AppRole.ADMIN)]
        [HttpPut(Name = "UpdateOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderVm>> UpdateOrder([FromBody] UpdateOrderCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = AppRole.ADMIN)]
        [HttpGet("list", Name = "PaginationOrder")]
        [ProducesResponseType(typeof(IReadOnlyList<OrderVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<OrderVm>>> PaginationOrder
            ([FromQuery] GetOrderQuery paginationOrdersParams)
        {
            var pagination = await _mediator.Send(paginationOrdersParams);
            return Ok(pagination);
        }

        [HttpGet("{id}", Name = "PaginationOrderById")]
        [ProducesResponseType(typeof(IReadOnlyList<OrderVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<OrderVm>>> PaginationOrderById
            ([FromRoute] int id,[FromQuery] GetOrderQuery paginationOrdersParams)
        {
            paginationOrdersParams.Id = id;
            var pagination = await _mediator.Send(paginationOrdersParams);
            return Ok(pagination);
        }
    }
}
