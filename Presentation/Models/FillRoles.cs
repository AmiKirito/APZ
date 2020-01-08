using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollutionReports.Models
{
    public class FillRoles
    {
        public static async Task Initialize(AppDbContext context, UserManager<User> userManager,
                                            RoleManager<ApplicationRole> roleManager)
        {
            context.Database.EnsureCreated();

            //string role = "Admin";
            string role2 = "CommonUser";
            string role3 = "Company";

            string description2 = "This is the common user role";
            string description3 = "This is the company role";
            //string description = "This is the admin role";

            //string password = "Emilia0Tan3";

            //if (await roleManager.FindByNameAsync(role) == null)
            //{
            //    await roleManager.CreateAsync(new ApplicationRole(role, description, DateTime.Now));
            //}

            //var user = new User
            //    {
            //        UserName = "AmiKirito",
            //        Email = "romankrol2000@gmail.com",
            //    };
            //    var result = await userManager.CreateAsync(user);
            //    if (result.Succeeded)
            //    {
            //        await userManager.AddPasswordAsync(user, password);
            //        await userManager.AddToRoleAsync(user, role);
            //    }

            await roleManager.CreateAsync(new ApplicationRole(role2, description2, DateTime.Now));
            await roleManager.CreateAsync(new ApplicationRole(role3, description3, DateTime.Now));
        }
    }
}
