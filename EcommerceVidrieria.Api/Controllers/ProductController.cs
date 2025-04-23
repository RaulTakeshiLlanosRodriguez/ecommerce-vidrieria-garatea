using CloudinaryDotNet.Actions;
using EcommerceVidrieria.Application.Contracts.Infrastructure;
using EcommerceVidrieria.Application.Features.Products.Command.CreateProduct;
using EcommerceVidrieria.Application.Features.Products.Command.UpdateAdminStatusProduct;
using EcommerceVidrieria.Application.Features.Products.Command.UpdateProduct;
using EcommerceVidrieria.Application.Features.Products.Queries.GetProductById;
using EcommerceVidrieria.Application.Features.Products.Queries.GetProductList;
using EcommerceVidrieria.Application.Features.Products.Vms;
using EcommerceVidrieria.Application.Models.Authorization;
using EcommerceVidrieria.Application.Models.ImageManagment;
using EcommerceVidrieria.Domain;
using EcommerceVidrieria.Infrastructure.ImageCloudinary;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EcommerceVidrieria.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IMediator _mediator;
        private IManageService _manageImageService;

        public ProductController(IMediator mediator, IManageService manageImageService)
        {
            _mediator = mediator;
            _manageImageService = manageImageService;
        }

        [Authorize(Roles = AppRole.ADMIN)]
        [HttpDelete("{id}", Name = "UpdateStatusProduct")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> UpdaStatusProduct(int id)
        {
            var request = new UpdateAdminStatusProductCommand(id);
            return await _mediator.Send(request);
        }

        [Authorize(Roles = AppRole.ADMIN)]
        [HttpGet("admin", Name = "PaginationProductAdmin")]
        [ProducesResponseType(typeof(IReadOnlyList<ProductVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<ProductVm>>> PaginationProductAdmin(
            [FromQuery] GetProductListQuery paginationProductosQuery
            )
        {
            var query = await _mediator.Send(paginationProductosQuery);
            return Ok(query);
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> GetProductById(int id)
        {
            var productById = new GetProductByIdQuery(id);
            var query = await _mediator.Send(productById);
            return Ok(query);
        }

        [AllowAnonymous]
        [HttpGet("list", Name = "PaginationProduct")]
        [ProducesResponseType(typeof(IReadOnlyList<ProductVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<ProductVm>>> PaginationProduct(
            [FromQuery] GetProductListQuery paginationProductosQuery
            )
        {
            paginationProductosQuery.Status = ProductStatus.Active;
            var query = await _mediator.Send(paginationProductosQuery);
            return Ok(query);
        }

        [Authorize(Roles = AppRole.ADMIN)]
        [HttpPost("create", Name = "CreateProduct")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> CreateProduct([FromForm] CreateProductCommand request)
        {
            var listImageUrls = new List<CreateProductImageVm>();

            if (request.Photos != null)
            {
                foreach (var image in request.Photos)
                {
                    var resultImage = await _manageImageService.UploadImage(
                        new ImageData
                        {
                            ImageStream = image.OpenReadStream(),
                            Name = image.Name
                        });

                    var imageCommand = new CreateProductImageVm
                    {
                        PublicCode = resultImage.PublicId,
                        Url = resultImage.Url
                    };

                    listImageUrls.Add(imageCommand);
                }

                request.ImageUrls = listImageUrls;
            }

            return await _mediator.Send(request);
        }

        [Authorize(Roles = AppRole.ADMIN)]
        [HttpPut("update", Name = "UpdateProduct")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> UpdateProduct([FromForm] UpdateProductCommand request)
        {
            var listImageUrls = new List<CreateProductImageVm>();

            if (request.Photos != null)
            {
                foreach (var image in request.Photos)
                {
                    var resultImage = await _manageImageService.UploadImage(
                        new ImageData
                        {
                            ImageStream = image.OpenReadStream(),
                            Name = image.Name
                        });

                    var imageCommand = new CreateProductImageVm
                    {
                        PublicCode = resultImage.PublicId,
                        Url = resultImage.Url
                    };

                    listImageUrls.Add(imageCommand);
                }

                request.ImageUrls = listImageUrls;
            }

            return await _mediator.Send(request);
        }
    }
}
