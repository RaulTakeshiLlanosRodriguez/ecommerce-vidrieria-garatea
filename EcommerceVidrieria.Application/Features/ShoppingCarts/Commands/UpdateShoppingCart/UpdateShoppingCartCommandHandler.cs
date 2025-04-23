using AutoMapper;
using EcommerceVidrieria.Application.Contracts.Identity;
using EcommerceVidrieria.Application.Exceptions;
using EcommerceVidrieria.Application.Features.ShoppingCarts.Vms;
using EcommerceVidrieria.Application.Persistence;
using EcommerceVidrieria.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.ShoppingCarts.Commands.UpdateShoppingCart
{
    public class UpdateShoppingCartCommandHandler : IRequestHandler<UpdateShoppingCartCommand, ShoppingCartVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public UpdateShoppingCartCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<ShoppingCartVm> Handle(UpdateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var userId = _authService.GetSessionUser();
            var shoppingCartToUpdate = await _unitOfWork.Repository<ShoppingCart>().GetEntityAsync(
                    p => p.UserId == userId);
            if (shoppingCartToUpdate == null)
            {
                throw new NotFoundException(nameof(ShoppingCart), _authService.GetSessionUser()!);
            }

            var shoppingCartItems = await _unitOfWork.Repository<ShoppingCartItem>().GetAsync(
                    x => x.ShoppingCartMasterId == shoppingCartToUpdate.ShoppingCartMasterId
                );

            _unitOfWork.Repository<ShoppingCartItem>().DeleteRange(shoppingCartItems);

            var shoppingCartItemsToAdd = _mapper.Map<List<ShoppingCartItem>>(request.ShoppingCartItems);

            shoppingCartItemsToAdd.ForEach(
                 x =>
                 {
                     x.ShoppingCartId = shoppingCartToUpdate.Id;
                     x.ShoppingCartMasterId = shoppingCartToUpdate.ShoppingCartMasterId;
                 });

            _unitOfWork.Repository<ShoppingCartItem>().AddRange(shoppingCartItemsToAdd);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception("No se pudo agregar productos al carrito");
            }

            var includes = new List<Expression<Func<ShoppingCart, object>>>();
            includes.Add(p => p.ShoppingCartItems!);
            var shoppingCart = await _unitOfWork.Repository<ShoppingCart>().GetEntityAsync(
                x => x.ShoppingCartMasterId == shoppingCartToUpdate.ShoppingCartMasterId,
                includes,
                true);

            return _mapper.Map<ShoppingCartVm>(shoppingCart);
        }
    }
}
