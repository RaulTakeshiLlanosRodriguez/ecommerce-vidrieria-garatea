using Azure.Core;
using EcommerceVidrieria.Application.Features.Auths.Users.Vms;
using EcommerceVidrieria.Application.Features.Orders.Vms;
using EcommerceVidrieria.Application.Features.Products.Vms;
using EcommerceVidrieria.Application.Features.Reports.Queries.GetMostSoldProductOfTheMonth;
using EcommerceVidrieria.Application.Features.Reports.Queries.GetSalesByMonth;
using EcommerceVidrieria.Application.Features.Reports.Queries.GetSoldProductsOfTheMonth;
using EcommerceVidrieria.Application.Features.Reports.Queries.GetTopOrdersByDate;
using EcommerceVidrieria.Application.Features.Reports.Queries.GetTopProductByValorization;
using EcommerceVidrieria.Application.Features.Reports.Queries.GetTotalOrders;
using EcommerceVidrieria.Application.Features.Reports.Queries.GetTotalOrdersOnCompletedOrders;
using EcommerceVidrieria.Application.Features.Reports.Queries.GetTotalSalesByMonth;
using EcommerceVidrieria.Application.Features.Reports.Queries.GetTotalUserByMonth;
using EcommerceVidrieria.Application.Features.Reports.Queries.GetUserWithHighestPurchase;
using EcommerceVidrieria.Application.Features.Reports.Vms;
using EcommerceVidrieria.Application.Models.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EcommerceVidrieria.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = AppRole.ADMIN)]
        [HttpGet("totalUsers", Name = "GetTotalUserByMonth")]
        [ProducesResponseType(typeof(ReportVm<int>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ReportVm<int>>> GetTotalUserByMonth([FromQuery] GetTotalUserByMonthQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("topProducts", Name = "GetTopProductsByValorization")]
        [ProducesResponseType(typeof(ReportVm<List<ProductVm>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ReportVm<List<ProductVm>>>> GetTopProductsByValorization()
        {
            var query = new GetTopProductByValorizationQuery();
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Roles = AppRole.ADMIN)]
        [HttpGet("topOrders", Name = "GetTopOrdersByDate")]
        [ProducesResponseType(typeof(ReportVm<List<OrderVm>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ReportVm<List<OrderVm>>>> GetTopOrdersByDate()
        {
            var query = new GetTopOrdersByDateQuery();
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Roles = AppRole.ADMIN)]
        [HttpGet("topSalesByMonth", Name = "GetTotalSalesByMonth")]
        [ProducesResponseType(typeof(GetTotalSalesByMonthVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetTotalSalesByMonthVm>> GetTotalSalesByMonth([FromQuery] GetTotalSalesByMonthQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Roles = AppRole.ADMIN)]
        [HttpGet("totalOrdersOnCompletedOrders", Name = "GetTotalOrdersOnCompletedOrders")]
        [ProducesResponseType(typeof(ReportVm<decimal>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ReportVm<decimal>>> GetTotalOrdersOnCompletedOrders([FromQuery] GetTotalOrdersOnCompletedOrdersQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Roles = AppRole.ADMIN)]
        [HttpGet("userHighestPurchase", Name = "GetUserWithHighestPurchase")]
        [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> GetUserWithHighestPurchase()
        {
            var query = new GetUserWithHighestPurchaseQuery();
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Roles = AppRole.ADMIN)]
        [HttpGet("mostSoldProduct", Name = "GetMostSoldProductOfTheMonth")]
        [ProducesResponseType(typeof(ProductVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> GetMostSoldProductOfTheMonth([FromQuery] GetMostSoldProductOfTheMonthQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Roles = AppRole.ADMIN)]
        [HttpGet("soldProducts", Name = "GetSoldProductsOfTheMonth")]
        [ProducesResponseType(typeof(ProductVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> GetSoldProductsOfTheMonth([FromQuery] GetSoldProductsOfTheMonthQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Roles = AppRole.ADMIN)]
        [HttpGet("salesByMonth", Name = "GetSalesByMonth")]
        [ProducesResponseType(typeof(List<SalesByMonthVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<SalesByMonthVm>>> GetSalesByMonth([FromQuery] GetSalesByMonthQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Roles = AppRole.ADMIN)]
        [HttpGet("totalOrders", Name = "GetTotalOrders")]
        [ProducesResponseType(typeof(TotalOrdersVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<TotalOrdersVm>> GetTotalOrders()
        {
            var query = new GetTotalOrdersQuery();
            return Ok(await _mediator.Send(query));
        }
    }
}
