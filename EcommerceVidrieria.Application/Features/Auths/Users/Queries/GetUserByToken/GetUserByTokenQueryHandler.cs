using CloudinaryDotNet.Core;
using EcommerceVidrieria.Application.Contracts.Identity;
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

namespace EcommerceVidrieria.Application.Features.Auths.Users.Queries.GetUserByToken
{
    public class GetUserByTokenQueryHandler : IRequestHandler<GetUserByTokenQuery, AuthResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByTokenQueryHandler(UserManager<User> userManager, IAuthService authService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _authService = authService;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResponse> Handle(GetUserByTokenQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(_authService.GetSessionUser());
            if (user == null)
            {
                throw new Exception("El usuario no esta autenticado");
            }

            if (!user.IsActive)
            {
                throw new Exception("El usuario esta de baja");
            }

            var roles = await _userManager.GetRolesAsync(user);

            return new AuthResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Lastname = user.LastName,
                Phone = user.PhoneNumber,
                Email = user.Email,
                Roles = roles,
                Token = _authService.CreateToken(user, roles)
            };
        }
    }
}
