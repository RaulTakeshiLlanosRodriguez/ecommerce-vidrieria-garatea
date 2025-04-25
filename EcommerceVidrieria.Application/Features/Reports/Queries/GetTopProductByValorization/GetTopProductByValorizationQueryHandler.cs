using AutoMapper;
using EcommerceVidrieria.Application.Features.Products.Vms;
using EcommerceVidrieria.Application.Features.Reports.Vms;
using EcommerceVidrieria.Application.Persistence;
using EcommerceVidrieria.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Reports.Queries.GetTopProductByValorization
{
    public class GetTopProductByValorizationQueryHandler : IRequestHandler<GetTopProductByValorizationQuery, ReportVm<List<ProductVm>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTopProductByValorizationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReportVm<List<ProductVm>>> Handle(GetTopProductByValorizationQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Product, object>>>();
            includes.Add(p => p.Images!);
            var productsActive = await _unitOfWork.Repository<Product>().GetAsync(x => x.Status == ProductStatus.Active,null, includes);
            var topProducts = productsActive.OrderByDescending(p => p.Valorization).Take(2).ToList();

            var result = new ReportVm<List<ProductVm>>
            {
                Result = _mapper.Map<List<ProductVm>>(topProducts)
            };

            return result;
                
        }
    }
}
