using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PollutionReports.Interfaces;
using PollutionReports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PollutionReports.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace PollutionReports.Controllers
{
    public class ReportController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly ICompanyRepository companyRepository;
        private readonly IReportRepository reportRepository;
        private readonly ISurveyRepository surveyRepository;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IWebHostEnvironment hostingEnvironment;

        public ReportController(AppDbContext dbContext, ICompanyRepository companyRepository,
                              IReportRepository reportRepository, ISurveyRepository surveyRepository,
                              UserManager<User> userManager, SignInManager<User> signInManager,
                              IWebHostEnvironment hostingEnvironment)
        {
            this.dbContext = dbContext;
            this.companyRepository = companyRepository;
            this.reportRepository = reportRepository;
            this.surveyRepository = surveyRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.hostingEnvironment = hostingEnvironment;
        }
        [Authorize(Roles = "Admin, Company, CommonUser")]
        [HttpGet]
        public IActionResult Index(int id, DateTime? startDate, DateTime? endDate)
        {
            List<Report> reports = dbContext.Reports.Include(r => r.Company).Where(r => r.CompanyId == id).ToList();
            SearchReportViewModel model = new SearchReportViewModel();
            if (startDate != null && endDate != null)
            {
                reports = reports.Where(r => r.SubmissionDate >= startDate && r.SubmissionDate <= endDate).ToList();
                model.reports = reports;
                return View(model);
            }
            else if(startDate != null)
            {
                reports = reports.Where(r => r.SubmissionDate >= startDate).ToList();
                model.reports = reports;
                return View(model);
            }
            else if(endDate != null)
            {
                reports = reports.Where(r => r.SubmissionDate <= endDate).ToList();
                model.reports = reports;
                return View(model);
            }
            else
            {
                model.reports = reports;
                return View(model);
            }
        }

        [Authorize(Roles = "Admin, Company")]
        [HttpGet]
        public IActionResult Add()
        {
            CreateReportViewModel model = new CreateReportViewModel();
            model.Companies = dbContext.Companies.Include(c => c.Owner).ToList();
            return View(model);
        }
        [Authorize(Roles = "Admin, Company")]
        [HttpPost]
        public IActionResult Add(CreateReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                Report report = ProcessUploadedFile(model);

                report.Title = model.Title;
                report.CompanyId = dbContext.Companies.Where(c => c.Title == model.CompanyName).FirstOrDefault().Id;
                report.SubmissionDate = DateTime.Today;

                dbContext.SaveChanges();
                reportRepository.Add(report);

                return RedirectToAction("Details", "Report", new { id = report.Id });
            }
            return View(model);
        }
        [Authorize(Roles = "Admin, Company, CommonUser")]
        public IActionResult Details(int id)
        {
            Report report = dbContext.Reports.Include(r => r.Company).Where(r => r.Id == id).FirstOrDefault();
            return View(report);
        }
        [Authorize(Roles = "Admin, Company")]
        public IActionResult Delete(int id)
        {
            reportRepository.DeleteReport(id);
            return RedirectToAction("Index", "Company");
        }

        [Authorize(Roles = "Admin, Company, CommonUser")]
        public IActionResult GetFile(int id)
        {
            Report report = dbContext.Reports.Include(r => r.Company).Where(r => r.Id == id).FirstOrDefault();
            string file_path = Path.Combine(hostingEnvironment.WebRootPath, "documents", report.DocumentPath);
            string file_type = "application/vnd.ms-word";
            string file_name = "report.docx";
            return PhysicalFile(file_path, file_type, file_name);
        }
        private Report ProcessUploadedFile(CreateReportViewModel model)
        {
            Report report = new Report();

            if (model.Document != null)
            {
                string uniquieFileName = "";
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "documents");
                uniquieFileName = Guid.NewGuid().ToString() + "_" + model.Document.FileName;
                string filePath = Path.Combine(uploadsFolder, uniquieFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Document.CopyTo(fileStream);
                }

                report.DocumentPath = uniquieFileName;
            }

            return report;
        }
    }
}
