using AutoMapper;
using EcommerceVidrieria.Application.Contracts.Identity;
using EcommerceVidrieria.Application.Exceptions;
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
            var subtotal = 0m;
            var total = 0m;
            var userId = _authService.GetSessionUser();

            var order = new Order
            {
                Subtotal = subtotal,
                UserId = userId,
                TotalOrder = total,
                Dni = request.Dni,
                DeliveryMethod = request.DeliveryMethod,
                PriceDelivery = request.PriceDelivery,
                CityId = request.CityId,
                Address = request.Address,
                CreatedDate = DateTime.Now,
                PhoneNumber = request.PhoneNumber
            };

            await _unitOfWork.Repository<Order>().AddAsync(order);

            var items = new List<OrderItem>();
            foreach(var orderItem in request.OrderItems!)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(orderItem.ProductId);
                if(product == null)
                {
                    throw new NotFoundException("Producto no encontrado", orderItem.ProductId);
                }
                var item = new OrderItem
                {
                    ProductId = orderItem.ProductId,
                    Quantity = orderItem.Quantity,
                    Price = product!.Price,
                    OrderId = order.Id
                };

                subtotal += orderItem.Quantity * product.Price;
                items.Add(item);
            }
            order.Subtotal = subtotal;
            order.TotalOrder = subtotal + request.PriceDelivery;
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
