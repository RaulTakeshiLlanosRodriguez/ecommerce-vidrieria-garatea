using AutoMapper;
using EcommerceVidrieria.Application.Contracts.Identity;
using EcommerceVidrieria.Application.Features.Orders.Vms;
using EcommerceVidrieria.Application.Persistence;
using EcommerceVidrieria.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authService = authService;
            _userManager = userManager;
        }

        public async Task<OrderVm> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var subtotal = Math.Round(request.OrderItems!.Sum(x => x.Price * x.Quantity), 2);
            var total = subtotal + request.PriceDelivery;
            var userId = _authService.GetSessionUser();

            var order = new Order
            {
                Subtotal = subtotal,
                UserId = userId,
                TotalOrder = total,
                Dni = request.Dni,
                DeliveryMethod = request.DeliveryMethod,
                City = request.City,
                Address = request.Address,
                CreatedDate = DateTime.Now
            };

            await _unitOfWork.Repository<Order>().AddAsync(order);

            var items = new List<OrderItem>();
            foreach(var orderItem in request.OrderItems!)
            {
                var item = new OrderItem
                {
                    ProductId = orderItem.ProductId,
                    Quantity = orderItem.Quantity,
                    Price = orderItem.Price,
                    OrderId = order.Id
                };
                items.Add(item);
            }

            _unitOfWork.Repository<OrderItem>().AddRange(items);
            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception("Error creando la orden de compra");
            }


            return _mapper.Map<OrderVm>(order);


        }
    }
}
