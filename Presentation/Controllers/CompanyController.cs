using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PollutionReports.Interfaces;
using PollutionReports.Models;
using PollutionReports.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PollutionReports.Controllers
{
    public class CompanyController : Controller
    {

        private readonly AppDbContext dbContext;
        private readonly ICompanyRepository companyRepository;
        private readonly UserManager<User> userManager;

        public CompanyController(AppDbContext dbContext, ICompanyRepository companyRepository,
                              UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.companyRepository = companyRepository;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Admin, Company, CommonUser")]
        [HttpGet]
        public IActionResult Index()
        {
            var model = dbContext.Companies
                        .Include(c => c.Owner)
                        .ToArray();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            CompanyCreateViewModel model = new CompanyCreateViewModel();
            model.Countries = GetCountries();
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(CompanyCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.AssignedUser);

                Company company = new Company
                {
                    UserId = user.Id,
                    Title = model.Title,
                    Country = model.Country,
                    Town = model.Town,
                    Description = model.Description
                };

                companyRepository.Add(company, user);
                return RedirectToAction("Details", "Company", new { id = company.Id});
            }
            model.Countries = GetCountries();
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Remove(int id)
        {
            companyRepository.Delete(id);
            return RedirectToAction("Index","Company");
        }
        [Authorize(Roles = "Admin, Company, CommonUser")]
        public IActionResult Details(int id)
        {
            var model = dbContext.Companies.Include(c => c.Owner).Where(c => c.Id == id).FirstOrDefault();
            return View(model);
        }

        public List<string> GetCountries()
        {
            List<string> cultureList = new List<string>();
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);

            foreach (CultureInfo culture in cultures)
            {
                if (culture.LCID != 127)
                {
                    try
                    {
                        RegionInfo region = new RegionInfo(culture.LCID);
                        if (!(cultureList.Contains(region.EnglishName)))
                        {
                            cultureList.Add(region.EnglishName);
                        }
                    }
                    catch { }
                }
            }

            cultureList.Sort();

            return cultureList;
        }
    }
}
