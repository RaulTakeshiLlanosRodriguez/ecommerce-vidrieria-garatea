using AutoMapper;
using EcommerceVidrieria.Application.Exceptions;
using EcommerceVidrieria.Application.Features.ShoppingCarts.Vms;
using EcommerceVidrieria.Application.Persistence;
using EcommerceVidrieria.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.ShoppingCarts.Commands.DeleteShoppingCartItem
{
    public class DeleteShoppingCartItemCommandHandler : IRequestHandler<DeleteShoppingCartItemCommand, ShoppingCartVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteShoppingCartItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ShoppingCartVm> Handle(DeleteShoppingCartItemCommand request, CancellationToken cancellationToken)
        {
            var shoppingCartToUpdate = await _unitOfWork.Repository<ShoppingCart>().GetEntityAsync(
                    p => p.ShoppingCartMasterId == request.ShoppingCartId
                );

            if (shoppingCartToUpdate == null)
            {
                throw new NotFoundException(nameof(ShoppingCart), request.ShoppingCartId!);
            }

            var shoppingCartItems = await _unitOfWork.Repository<ShoppingCartItem>().GetAsync(
                    x => x.ShoppingCartMasterId == request.ShoppingCartId
                );

            _unitOfWork.Repository<ShoppingCartItem>().DeleteRange(shoppingCartItems);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception("No se pudo eliminar los productos del carrito");
            }

            var shoppingCart = await _unitOfWork.Repository<ShoppingCart>().GetEntityAsync(
                x => x.ShoppingCartMasterId == request.ShoppingCartId);

            return _mapper.Map<ShoppingCartVm>(shoppingCart);
        }
    }
}
