using AutoMapper;
using EcommerceVidrieria.Application.Features.Orders.Vms;
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

namespace EcommerceVidrieria.Application.Features.Reports.Queries.GetTopOrdersByDate
{
    public class GetTopOrdersByDateQueryHandler : IRequestHandler<GetTopOrdersByDateQuery, ReportVm<List<OrderVm>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTopOrdersByDateQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReportVm<List<OrderVm>>> Handle(GetTopOrdersByDateQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Order, object>>>();
            includes.Add(p => p.OrderItems!);
            var orders = await _unitOfWork.Repository<Order>().GetAsync(x => x.Status == OrderStatus.Pending,null,includes);
            var topOrders = orders.OrderByDescending(x => x.CreatedDate).Take(5).ToList();

            var result = new ReportVm<List<OrderVm>>
            {
                Result = _mapper.Map<List<OrderVm>>(topOrders)
            };

            return result;
        }
    }
}
