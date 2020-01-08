using Microsoft.AspNetCore.Http;
using PollutionReports.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PollutionReports.ViewModels
{
    public class CreateReportViewModel
    {
        [Required(ErrorMessage = "The report title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "The report company name is required")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "The report Document must be attached")]
        public IFormFile Document { get; set; }
        public DateTime SubmissionDate { get; set; }
        public List<Company> Companies { get; set; }
    }
}
