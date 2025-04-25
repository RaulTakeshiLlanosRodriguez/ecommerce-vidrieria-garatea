using EcommerceVidrieria.Application.Features.Auths.Users.Vms;
using EcommerceVidrieria.Application.Persistence;
using EcommerceVidrieria.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Reports.Queries.GetUserWithHighestPurchase
{
    public class GetUserWithHighestPurchaseQueryHandler : IRequestHandler<GetUserWithHighestPurchaseQuery, AuthResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public GetUserWithHighestPurchaseQueryHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<AuthResponse> Handle(GetUserWithHighestPurchaseQuery request, CancellationToken cancellationToken)
        {
            var completedOrders = await _unitOfWork.Repository<Order>().GetAsync(x => x.Status == OrderStatus.Completed);
            var userTotalPurchases = completedOrders.GroupBy(o => o.UserId).Select(

                    group => new
                    {
                        UserId = group.Key,
                        TotalPurchases = group.Sum(o => o.TotalOrder)
                    }).OrderByDescending(o => o.TotalPurchases).FirstOrDefault();

            if (userTotalPurchases == null)
            {
                throw new Exception("No se encontraron órdenes completadas");
            }

            var user = await _userManager.FindByIdAsync(userTotalPurchases.UserId!);

            return new AuthResponse
            {
                Id = user!.Id,
                UserName = user.UserName,
                Email = user.Email,
                Lastname = user.LastName,
                Roles = await _userManager.GetRolesAsync(user)
            };
        }
    }
}
