using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollutionReports.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
    }
}
