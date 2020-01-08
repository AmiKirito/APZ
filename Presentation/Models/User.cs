using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollutionReports.Models
{
    public class User : IdentityUser
    {
        public User() : base() { }
        public List<Survey> Surveys { get; set; }
        public List<Company> Companies { get; set; }
    }
}
