using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace PollutionReports.ViewModels
{
    public class UserViewModel
    {
            public string Id { get; set; }
            public string Username { get; set; }
            public List<string> Roles { get; set; }
    }
}
