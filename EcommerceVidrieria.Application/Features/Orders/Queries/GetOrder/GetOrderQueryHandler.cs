using AutoMapper;
using EcommerceVidrieria.Application.Features.Orders.Vms;
using EcommerceVidrieria.Application.Persistence;
using EcommerceVidrieria.Application.Specifications.Orders;
using EcommerceVidrieria.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Orders.Queries.GetOrder
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, IReadOnlyList<OrderVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrderQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<OrderVm>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var orderSpecificationParams = new OrderSpecificationParams
            {
                Id = request.Id,
                UserId = request.UserId
            };

            var spec = new OrderSpecification(orderSpecificationParams);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpec(spec);

            var data = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderVm>>(orders);

            return data;
        }
    }
}
