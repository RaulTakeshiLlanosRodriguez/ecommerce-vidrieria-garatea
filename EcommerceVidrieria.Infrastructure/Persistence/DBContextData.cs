using CloudinaryDotNet.Actions;
using EcommerceVidrieria.Application.Models.Authorization;
using EcommerceVidrieria.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Infrastructure.Persistence
{
    public class DBContextData
    {
        public static async Task LoadDataAsync(
            DBContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ILoggerFactory loggerFactory
            )
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole(AppRole.ADMIN));
                    await roleManager.CreateAsync(new IdentityRole(AppRole.USER));
                }
                if (!userManager.Users.Any())
                {
                    var userAdmin = new User
                    {
                        UserName = "Raul",
                        LastName = "Llanos",
                        Email = "r.llanosrod@gmail.com",
                        CreatedDate = DateTime.Now
                    };
                    await userManager.CreateAsync(userAdmin, "PasswordRaulLlanos123$");
                    await userManager.AddToRoleAsync(userAdmin, AppRole.ADMIN);
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<DBContextData>();
                logger.LogError(ex.Message);
            }
        }
    }
}
