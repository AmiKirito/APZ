using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PollutionReports.Models;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using PollutionReports.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using PollutionReports.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Data.SqlClient;

namespace PollutionReports.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public HomeController(AppDbContext dbContext, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        [Authorize(Roles = "Admin, Company, CommonUser")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username, Email = model.Email };

                string role = model.Role;

                string description = $"This is the {role} role";

                IdentityResult result = new IdentityResult();
                if(model.Role == "Company")
                {
                    user.PhoneNumberConfirmed = true;
                }
                result = await userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, model.Password);
                    await userManager.AddToRoleAsync(user, role);
                    return RedirectToAction("UserList", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            Collection<UserViewModel> model = new Collection<UserViewModel>();
            foreach (var user in userManager.Users)
            {
                UserViewModel currUser = new UserViewModel();

                var userRoles = await userManager.GetRolesAsync(user);
                currUser.Id = user.Id;
                currUser.Username = user.UserName;
                currUser.Roles = userRoles.ToList();
                model.Add(currUser);
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveUser(string userId)
        {
            var user = dbContext.Users.Find(userId);
            if(user.UserName == User.Identity.Name)
            {
                ViewBag.Error = "Unable to remove active user";
                return RedirectToAction("UserList", "Home");
            }
            else
            {
                dbContext.Users.Remove(user);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("UserList", "Home");
            }
        }

    }
}
