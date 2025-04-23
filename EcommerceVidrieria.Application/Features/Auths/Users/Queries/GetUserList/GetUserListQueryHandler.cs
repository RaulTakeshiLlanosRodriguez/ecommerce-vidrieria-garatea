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

namespace EcommerceVidrieria.Application.Features.Auths.Users.Queries.GetUserList
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, IReadOnlyList<AuthResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public GetUserListQueryHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IReadOnlyList<AuthResponse>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.Repository<User>().GetAllAsync();

            var authResponses = new List<AuthResponse>();

            foreach (var user in users)
            {
                var authResponse = new AuthResponse
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Lastname = user.LastName,
                    Email = user.Email,
                    Roles = await _userManager.GetRolesAsync(user)
                };

                authResponses.Add(authResponse);
            }
            return authResponses;
        }
    }
}
