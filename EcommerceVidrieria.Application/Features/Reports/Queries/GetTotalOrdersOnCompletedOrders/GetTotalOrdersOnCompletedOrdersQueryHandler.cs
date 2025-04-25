using EcommerceVidrieria.Application.Features.Reports.Vms;
using EcommerceVidrieria.Application.Persistence;
using EcommerceVidrieria.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Reports.Queries.GetTotalOrdersOnCompletedOrders
{
    public class GetTotalOrdersOnCompletedOrdersQueryHandler : IRequestHandler<GetTotalOrdersOnCompletedOrdersQuery, GetTotalOrdersOnCompletedOrdersVm>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTotalOrdersOnCompletedOrdersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetTotalOrdersOnCompletedOrdersVm> Handle(GetTotalOrdersOnCompletedOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Repository<Order>().GetAsync(x => x.Status != OrderStatus.Cancelled && x.CreatedDate!.Value.Month == request.Month && x.CreatedDate!.Value.Year == request.Year);

            var totalOrder = orders.Count();
            var pendingOrders = orders.Count(o => o.Status  == OrderStatus.Pending);
            var completedOrders = orders.Count(o => o.Status == OrderStatus.Completed);

            var percentage = totalOrder > 0 ? (decimal)completedOrders / totalOrder * 100 : 0;

            return new GetTotalOrdersOnCompletedOrdersVm
            {
                TotalOrdersCurrentMonth = totalOrder,
                CompletedOrders = completedOrders,
                PendingOrders = pendingOrders,
                CompletionPercentage = percentage
            };
        }
    }
}
