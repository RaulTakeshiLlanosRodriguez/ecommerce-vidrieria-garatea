using CloudinaryDotNet.Actions;
using EcommerceVidrieria.Application.Features.Auths.Roles.Queries.GetRoles;
using EcommerceVidrieria.Application.Features.Auths.Users.Commands.LoginUser;
using EcommerceVidrieria.Application.Features.Auths.Users.Commands.RegisterUser;
using EcommerceVidrieria.Application.Features.Auths.Users.Commands.ResetPasswordByToken;
using EcommerceVidrieria.Application.Features.Auths.Users.Commands.UpdateAdminStatusUser;
using EcommerceVidrieria.Application.Features.Auths.Users.Commands.UpdateProfileUser;
using EcommerceVidrieria.Application.Features.Auths.Users.Queries.GetUserById;
using EcommerceVidrieria.Application.Features.Auths.Users.Queries.GetUserByIdAdmin;
using EcommerceVidrieria.Application.Features.Auths.Users.Queries.GetUserByToken;
using EcommerceVidrieria.Application.Features.Auths.Users.Queries.GetUserList;
using EcommerceVidrieria.Application.Features.Auths.Users.Vms;
using EcommerceVidrieria.Application.Models.Authorization;
using EcommerceVidrieria.Domain;
using EcommerceVidrieria.Infrastructure.ImageCloudinary;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EcommerceVidrieria.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("login", Name = "Login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginUserCommand request)
        {
            return await _mediator.Send(request);
        }

        [AllowAnonymous]
        [HttpPost("register", Name = "Register")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterUserCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost("resetPassword", Name = "ResetPassword")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> ResetPassword([FromBody] ResetPasswordByTokenCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = AppRole.ADMIN)]
        [HttpPut("updateAdminStatusUser", Name = "UpdateAdminStatusUser")]
        [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> UpdateAdminStatusUser([FromBody] UpdateAdminStatusUserCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("{id}", Name = "GetUserById")]
        [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> GetUserById(string id)
        {
            var query = new GetUserByIdQuery(id);
            return await _mediator.Send(query);
        }

        [HttpGet("admin/{id}", Name = "GetUserByIdAdmin")]
        [ProducesResponseType(typeof(GetUserByIdAdminVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetUserByIdAdminVm>> GetUserByIdAdmin(string id)
        {
            var query = new GetUserByIdAdminQuery(id);
            return await _mediator.Send(query);
        }

        [HttpPut("update", Name = "Update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> Update([FromBody] UpdateProfileUserCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("", Name = "CurrentUser")]
        [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> CurrentUser()
        {
            var query = new GetUserByTokenQuery();
            return await _mediator.Send(query);
        }

        [Authorize(Roles = AppRole.ADMIN)]
        [HttpGet("paginationAdmin", Name = "PaginationUser")]
        [ProducesResponseType(typeof(IReadOnlyList<AuthResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<AuthResponse>>> PaginationUser([FromQuery] GetUserListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [AllowAnonymous]
        [HttpGet("roles", Name = "GetRolesList")]
        [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<string>>> GetRolesList()
        {
            var query = new GetRolesQuery();
            return Ok(await _mediator.Send(query));
        }
    }
}
