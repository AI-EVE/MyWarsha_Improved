using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyWarsha_Models.Models;

namespace MyWarsha_DataAccess.Data.AdminDataSeed
{
    public class AdminDataSeed
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<AppUser> userManager)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roleNames = { "Admin", "User" };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        var adminUser = new AppUser
        {
            UserName = "admin",
            Email = "admin@Warsha.com"
        };

        string adminPassword = "012015ssSS!";
        var user = await userManager.FindByEmailAsync(adminUser.Email);

        if (user == null)
        {
            var createAdminUser = await userManager.CreateAsync(adminUser, adminPassword);
            if (createAdminUser.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
    }
}