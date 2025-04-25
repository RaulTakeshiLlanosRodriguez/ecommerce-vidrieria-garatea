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

namespace EcommerceVidrieria.Application.Features.Auths.Users.Queries.GetUserByIdAdmin
{
    public class GetUserByIdAdminQueryHandler : IRequestHandler<GetUserByIdAdminQuery, GetUserByIdAdminVm>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByIdAdminQueryHandler(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetUserByIdAdminVm> Handle(GetUserByIdAdminQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId!);

            if (user == null)
            {
                throw new Exception("El usuario no existe");
            }

            var orders = await _unitOfWork.Repository<Order>().GetAsync(x => x.UserId == user.Id);
            var ordersCompleted = await _unitOfWork.Repository<Order>().GetAsync(x => x.Status == OrderStatus.Completed && x.UserId == user.Id);
            var totalOrder = orders.Count();
            var pendingOrders = orders.Count(o => o.Status == OrderStatus.Pending);
            var completedOrders = orders.Count(o => o.Status == OrderStatus.Completed);
            var cancelledOrders = orders.Count(o => o.Status == OrderStatus.Cancelled);
            var totalBuy = ordersCompleted.Sum(x => x.TotalOrder);

            return new GetUserByIdAdminVm
            {
                Id = user.Id,
                UserName = user.UserName,
                Lastname = user.LastName,
                Email = user.Email,
                Roles = await _userManager.GetRolesAsync(user),
                CreatedDate = user.CreatedDate.ToString("dd/MM/yy"),
                TotalOrders = totalOrder,
                 PendingOrders = pendingOrders,
                 CompletedOrders = completedOrders,
                 CancelledOrders = cancelledOrders,
                 TotalBuy = totalBuy                 
            };
        }
    }
}
