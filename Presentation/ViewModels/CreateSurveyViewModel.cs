using PollutionReports.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PollutionReports.ViewModels
{
    public class CreateSurveyViewModel
    {
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
