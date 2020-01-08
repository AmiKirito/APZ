using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollutionReports.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User Owner { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public string Town { get; set; }
        public string Description { get; set; }
        public List<Report> Reports { get; set; }
        public List<Survey> Surveys { get; set; }
    }
}
