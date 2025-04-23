using AutoMapper;
using EcommerceVidrieria.Application.Exceptions;
using EcommerceVidrieria.Application.Features.Orders.Vms;
using EcommerceVidrieria.Application.Features.Products.Vms;
using EcommerceVidrieria.Application.Persistence;
using EcommerceVidrieria.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderVm> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(request.Orderid);
            
            if(request.Status == OrderStatus.Completed)
            {
                foreach(var orderItem in order.OrderItems!)
                {
                    var productToUpdate = await _unitOfWork.Repository<Product>().GetByIdAsync(orderItem.ProductId);

                    if(productToUpdate == null)
                    {
                        throw new NotFoundException(nameof(Product), orderItem.ProductId);
                    }

                    if(productToUpdate.Stock >= orderItem.Quantity)
                    {
                        productToUpdate.Stock -= orderItem.Quantity;
                    }
                    else
                    {
                        throw new Exception($"No hay suficiente stock para el producto {productToUpdate.Name}.");
                    }

                    productToUpdate.Valorization += 1;

                    _unitOfWork.Repository<Product>().UpdateEntity(productToUpdate);
                }
            }
            
            order.Status = request.Status;

            _unitOfWork.Repository<Order>().UpdateEntity(order);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception("No se pudo actualizar el status de la orden de compra");
            }


            return _mapper.Map<OrderVm>(order);
        }
    }
}
