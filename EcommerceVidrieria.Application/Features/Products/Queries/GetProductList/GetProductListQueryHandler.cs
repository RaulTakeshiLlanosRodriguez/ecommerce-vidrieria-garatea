using AutoMapper;
using EcommerceVidrieria.Application.Features.Products.Vms;
using EcommerceVidrieria.Application.Persistence;
using EcommerceVidrieria.Application.Specifications.Products;
using EcommerceVidrieria.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Products.Queries.GetProductList
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, IReadOnlyList<ProductVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ProductVm>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var productSpecificationParams = new ProductSpecificationParams
            {
                CategoryId = request.CategoryId,
                Status = request.Status
            };

            var spec = new ProductSpecification(productSpecificationParams);
            var products = await _unitOfWork.Repository<Product>().GetAllWithSpec(spec);

            var data = _mapper.Map<IReadOnlyList<ProductVm>>(products);

            return data;
        }
    }
}
