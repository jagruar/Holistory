using Holistory.Common.Constants;
using Holistory.Domain.Aggregates.UserAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Holistory.Api.Application.Data
{
    internal static class IdentityDataSeed
    {
        public async static Task Seed(IServiceProvider services, string adminUsername, string adminPassword)
        {
            await SeedIdentityRoles(services);
            await SeedInitialUsers(services, adminUsername, adminPassword);
        }

        private async static Task SeedIdentityRoles(IServiceProvider services)
        {
            using (IServiceScope scope = services.CreateScope())
            {
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                foreach (string role in IdentityRoles.GetAll())
                {
                    bool roleExists = await roleManager.RoleExistsAsync(role);

                    if (!roleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }            
        }

        private async static Task SeedInitialUsers(IServiceProvider services, string adminUsername, string adminPassword)
        {
            using (IServiceScope scope = services.CreateScope())
            {
                UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                if (!userManager.Users.Any())
                {
                    User admin = new User(adminUsername, "admin@admin.com");
                    IdentityResult result = await userManager.CreateAsync(admin, adminPassword);

                    if (result.Succeeded)
                    {
                        User user = Task.Run(async () => await userManager.FindByNameAsync(adminUsername)).Result;
                        await userManager.AddToRoleAsync(user, IdentityRoles.Admin);
                        await userManager.AddToRoleAsync(user, IdentityRoles.User);
                    }
                }
            }
        }
    }
}
