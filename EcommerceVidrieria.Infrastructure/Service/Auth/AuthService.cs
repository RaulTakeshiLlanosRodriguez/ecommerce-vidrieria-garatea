using EcommerceVidrieria.Application.Contracts.Identity;
using EcommerceVidrieria.Application.Models.Token;
using EcommerceVidrieria.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Infrastructure.Service.Auth
{
    public class AuthService : IAuthService
    {
        public JwtSettings _jwtSettings { get; }
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IOptions<JwtSettings> jwtSettings, IHttpContextAccessor httpContextAccessor)
        {
            _jwtSettings = jwtSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public string CreateToken(User user, IList<string>? roles)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName!),
                new Claim("userId", user.Id),
                new Claim("email", user.Email!)

            };

            foreach (var rol in roles!)
            {
                var claim = new Claim(ClaimTypes.Role, rol);
                claims.Add(claim);
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.ExpireTime),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }

        public string GetSessionUser()
        {
            var userId = _httpContextAccessor.HttpContext!.User?.Claims?
                .FirstOrDefault(x => x.Type == "userId")?.Value;
            return userId!;
        }
    }
}
