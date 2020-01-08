using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PollutionReports.Interfaces;
using PollutionReports.Models;
using PollutionReports.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollutionReports.Controllers
{
    public class SurveyController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly ICompanyRepository companyRepository;
        private readonly IReportRepository reportRepository;
        private readonly ISurveyRepository surveyRepository;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public SurveyController(AppDbContext dbContext, ICompanyRepository companyRepository,
                              IReportRepository reportRepository, ISurveyRepository surveyRepository,
                              UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.dbContext = dbContext;
            this.companyRepository = companyRepository;
            this.reportRepository = reportRepository;
            this.surveyRepository = surveyRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [Authorize(Roles = "Admin, Company, CommonUser")]
        [HttpGet]
        public IActionResult Index(int? id)
        {
            List<Survey> model = dbContext.Surveys.Include(s => s.Company).Where(s => s.CompanyId == id).ToList();
            (List<Survey> surveys, string CompanyName) surModel;
            surModel.surveys = model;
            if(id == null)
            {
                surModel.CompanyName = dbContext.Companies.Include(c => c.Owner).FirstOrDefault().Title;
            }
            else
            {
                surModel.CompanyName = dbContext.Companies.Include(c => c.Owner).Where(c => c.Id == id).FirstOrDefault().Title;
            }
            return View(surModel);
        }
        [Authorize(Roles = "Admin, Company, CommonUser")]
        [HttpGet]
        public IActionResult Details(int id)
        {
            Survey survey = dbContext.Surveys.Include(s => s.Company).Where(s => s.Id == id).FirstOrDefault();
            return View(survey);
        }
        [Authorize(Roles = "Admin, CommonUser")]
        [HttpGet]
        public IActionResult Add(int id)
        {
            CreateSurveyViewModel model = new CreateSurveyViewModel();
            model.CompanyName = dbContext.Companies.Include(c => c.Owner).Where(c => c.Id == id).FirstOrDefault().Title;
            return View(model);
        }
        [Authorize(Roles = "Admin, CommonUser")]
        [HttpPost]
        public async Task<IActionResult> Add(CreateSurveyViewModel model)
        {
            if (ModelState.IsValid)
            {
                Survey survey = new Survey();
                User user = await userManager.FindByNameAsync(model.Username);
                survey.UserId = user.Id;
                survey.Username = User.Identity.Name;
                survey.Company = dbContext.Companies.Where(c => c.Title == model.CompanyName).FirstOrDefault();
                survey.CompanyId = dbContext.Companies.Where(c => c.Title == model.CompanyName).FirstOrDefault().Id;
                survey.Content = model.Content;

                dbContext.SaveChanges();
                surveyRepository.Add(survey);

                return RedirectToAction("Details", "Survey", new { id = survey.Id });
            }
            return View();
        }
    }
}
