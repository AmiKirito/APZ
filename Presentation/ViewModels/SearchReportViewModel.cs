using Microsoft.AspNetCore.Mvc.Rendering;
using PollutionReports.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PollutionReports.ViewModels
{
    public class SearchReportViewModel
    {
        public List<Report> reports { get; set; }
        [Display(Name = "From: ")]
        public DateTime startDate { get; set; }
        [Display(Name = "To: ")]
        public DateTime endDate { get; set; }
    }
}
