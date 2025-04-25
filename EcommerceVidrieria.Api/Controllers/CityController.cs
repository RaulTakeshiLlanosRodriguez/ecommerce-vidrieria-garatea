using EcommerceVidrieria.Application.Features.Categories.Queries.GetCategoryList;
using EcommerceVidrieria.Application.Features.Categories.Vms;
using EcommerceVidrieria.Application.Features.Cities.Queries.GetCities;
using EcommerceVidrieria.Application.Features.Cities.Vms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EcommerceVidrieria.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private IMediator _mediator;

        public CityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("list", Name = "GetCities")]
        [ProducesResponseType(typeof(IReadOnlyList<CityVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<CityVm>>> GetCities()
        {
            var query = new GetCitiesQuery();
            return Ok(await _mediator.Send(query));
        }
    }
}
