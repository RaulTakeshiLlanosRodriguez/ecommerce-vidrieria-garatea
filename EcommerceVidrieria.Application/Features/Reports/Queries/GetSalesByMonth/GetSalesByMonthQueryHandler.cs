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

namespace EcommerceVidrieria.Application.Features.Reports.Queries.GetSalesByMonth
{
    public class GetSalesByMonthQueryHandler : IRequestHandler<GetSalesByMonthQuery, List<SalesByMonthVm>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSalesByMonthQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SalesByMonthVm>> Handle(GetSalesByMonthQuery request, CancellationToken cancellationToken)
        {
            var includesItems = new List<Expression<Func<Order, object>>>();
            includesItems.Add(p => p.OrderItems!);
            var orders = await _unitOfWork.Repository<Order>().GetAsync(x => x.Status == OrderStatus.Completed && x.CreatedDate!.Value.Year == request.Year,null, includesItems);
            var salesByMonth = new Dictionary<int, (int totalProducts, decimal totalSales)>();
            for (int i = 1; i <= 12; i++)
            {
                salesByMonth[i] = (totalProducts: 0, totalSales: 0m);
            }

            foreach (var order in orders)
            {
                var month = order.CreatedDate!.Value.Month;

                foreach (var orderItem in order.OrderItems!)
                {
                    salesByMonth[month] = (
                        salesByMonth[month].totalProducts + orderItem.Quantity,
                        salesByMonth[month].totalSales + order.TotalOrder);
                }
            }

            return salesByMonth.Select(monthData => new SalesByMonthVm
            {
                Month = GetMonthName(monthData.Key),
                TotalProductsByMonth = monthData.Value.totalProducts,
                TotalSalesByMonth = monthData.Value.totalSales
            }).ToList();
        }

        private string GetMonthName(int month)
        {
            var months = new[]
            {
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            };

            return months[month - 1];
        }
    }
}
