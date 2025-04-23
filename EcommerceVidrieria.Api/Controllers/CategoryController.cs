using EcommerceVidrieria.Application.Features.Categories.Queries.GetCategoryList;
using EcommerceVidrieria.Application.Features.Categories.Vms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EcommerceVidrieria.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("list", Name = "GetCategories")]
        [ProducesResponseType(typeof(IReadOnlyList<CategoryVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<CategoryVm>>> GetCategories()
        {
            var query = new GetCategoryListQuery();
            return Ok(await _mediator.Send(query));
        }
    }
}
