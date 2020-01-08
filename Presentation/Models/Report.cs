using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PollutionReports.Models;

namespace PollutionReports.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string DocumentPath { get; set; }
    }
}
