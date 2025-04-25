using EcommerceVidrieria.Application.Features.Reports.Vms;
using EcommerceVidrieria.Application.Persistence;
using EcommerceVidrieria.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Reports.Queries.GetTotalOrders
{
    public class GetTotalOrdersQueryHandler : IRequestHandler<GetTotalOrdersQuery, TotalOrdersVm>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTotalOrdersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TotalOrdersVm> Handle(GetTotalOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Repository<Order>().GetAllAsync();
            var totalOrders = orders.Count();
            var totalPendingOrders = orders.Count(o => o.Status == OrderStatus.Pending);
            var totalCompleteOrders = orders.Count(o => o.Status == OrderStatus.Completed);
            var totalCanceledOrders = orders.Count(o => o.Status == OrderStatus.Cancelled);

            var ordersWithCity = orders.Where(o => o.CityId.HasValue).ToList();
            var cityOrders = ordersWithCity
           .GroupBy(o => o.CityId)
           .Select(group => new CityOrdersVm
           {
               CityId = group.Key ?? 0,
               TotalCityOrders = group.Count()
           })
           .ToList();

            return new TotalOrdersVm
            {
                TotalOrders = totalOrders,
                TotalPendingOrders = totalPendingOrders,
                TotalCompletedOrders = totalCompleteOrders,
                TotalCancelledOrders = totalCanceledOrders,
                OrdersByCity = cityOrders
            };


        }
    }
}
