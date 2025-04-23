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

namespace EcommerceVidrieria.Application.Features.ShoppingCarts.Queries.GetCart
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, ShoppingCartVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public GetCartQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<ShoppingCartVm> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var userId = _authService.GetSessionUser();
            var includes = new List<Expression<Func<ShoppingCart, object>>>();
            includes.Add(p => p.ShoppingCartItems!);

            var shoppingCart = await _unitOfWork.Repository<ShoppingCart>().GetEntityAsync(
                x => x.UserId == userId,
                includes,
                true
            );

            if(shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    ShoppingCartMasterId = request.ShoppingCartId,
                    ShoppingCartItems = new List<ShoppingCartItem>(),
                    UserId = userId
                };

                _unitOfWork.Repository<ShoppingCart>().AddEntity(shoppingCart);
                await _unitOfWork.Complete();
                
            }

            return _mapper.Map<ShoppingCartVm>(shoppingCart);
        }
    }
}
