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

            string role = "Company";

            //string description = "This is the company role";

            string password = "Emilia0Tan3";

            //if(await roleManager.FindByNameAsync(role) == null)
            //{
            //    await roleManager.CreateAsync(new ApplicationRole(role, description, DateTime.Now));
            //}
                var user = new User
                {
                    UserName = "Apple",
                    Email = "apple@gmail.com",
                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role);
                }
        }
    }
}
