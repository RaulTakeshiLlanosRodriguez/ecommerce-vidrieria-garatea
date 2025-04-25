using AutoMapper;
using EcommerceVidrieria.Application.Features.Products.Vms;
using EcommerceVidrieria.Application.Persistence;
using EcommerceVidrieria.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Reports.Queries.GetMostSoldProductOfTheMonth
{
    public class GetMostSoldProductOfTheMonthQueryHandler : IRequestHandler<GetMostSoldProductOfTheMonthQuery, ProductVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMostSoldProductOfTheMonthQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductVm> Handle(GetMostSoldProductOfTheMonthQuery request, CancellationToken cancellationToken)
        {
            var includesItems = new List<Expression<Func<Order, object>>>();
            includesItems.Add(p => p.OrderItems!);
            var completedOrders = await _unitOfWork.Repository<Order>().GetAsync(x => x.Status == OrderStatus.Completed && x.CreatedDate!.Value.Month == request.Month && x.CreatedDate!.Value.Year == request.Year,null,includesItems);

            var orderItems = completedOrders.SelectMany(o => o.OrderItems!)
                .GroupBy(oi => oi.ProductId)
                .Select(
                    group => new
                    {
                        ProductId = group.Key,
                        TotalQuantity = group.Sum(oi => oi.Quantity)
                    }).OrderByDescending(x => x.TotalQuantity)
                    .FirstOrDefault();

            if(orderItems == null)
            {
                throw new Exception("No se encontraron productos vendidos este mes.");
            }
            var includes = new List<Expression<Func<Product, object>>>();
            includes.Add(p => p.Images!);
            includes.Add(p => p.Category!);
            var product = await _unitOfWork.Repository<Product>().GetEntityAsync(x => x.Id ==  orderItems.ProductId,includes,true);

            if (product == null)
            {
                throw new Exception("Producto no encontrado.");
            }

            return _mapper.Map<ProductVm>(product);

        }
    }
}
