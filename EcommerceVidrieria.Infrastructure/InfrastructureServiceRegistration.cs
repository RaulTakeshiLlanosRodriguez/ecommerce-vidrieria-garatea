using EcommerceVidrieria.Application.Contracts.Identity;
using EcommerceVidrieria.Application.Models.ImageManagment;
using EcommerceVidrieria.Application.Models.Token;
using EcommerceVidrieria.Application.Persistence;
using EcommerceVidrieria.Infrastructure.Repositories;
using EcommerceVidrieria.Infrastructure.Service.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddTransient<IAuthService, AuthService>();

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            return services;
        }
    }
}
