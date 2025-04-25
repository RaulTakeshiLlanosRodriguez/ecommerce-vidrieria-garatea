using EcommerceVidrieria.Application.Features.Reports.Vms;
using EcommerceVidrieria.Application.Persistence;
using EcommerceVidrieria.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Reports.Queries.GetTotalSalesByMonth
{
    public class GetTotalSalesByMonthQueryHandler : IRequestHandler<GetTotalSalesByMonthQuery, GetTotalSalesByMonthVm>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTotalSalesByMonthQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetTotalSalesByMonthVm> Handle(GetTotalSalesByMonthQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Repository<Order>().GetAsync(x => x.Status == OrderStatus.Completed && x.CreatedDate!.Value.Month == request.Month
            && x.CreatedDate!.Value.Year == request.Year);

            var totalSales = orders.Sum(o => o.TotalOrder);
            var totalOrders = orders.Count();
            var maxOrderSale = orders.Max(o =>  o.TotalOrder);
            var result = new GetTotalSalesByMonthVm
            {
                TotalSales = totalSales,
                TotalOrdersCurrentMonth = totalOrders,
                MaxOrderSale = maxOrderSale
            };
            return result;
        }
    }
}
