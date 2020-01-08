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
        [Required]
        public string Title { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public IFormFile Document { get; set; }
        public DateTime SubmissionDate { get; set; }
        public List<Company> Companies { get; set; }
    }
}
