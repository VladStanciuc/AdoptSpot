using AdoptSpot.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Data
{
    public static  class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Enums.UserRole.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.UserRole.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.UserRole.NormalUser.ToString()));
        }
        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin@gmail.com",
                Email = "superadmin@gmail.com",
                FirstName = "Vlad",
                LastName = "Stanciuc",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123456!");
                    await userManager.AddToRoleAsync(defaultUser, Enums.UserRole.SuperAdmin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.UserRole.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.UserRole.NormalUser.ToString());
                    
                 
                }

            }
        }
    }
}
