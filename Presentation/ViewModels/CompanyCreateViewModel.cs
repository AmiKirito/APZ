using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PollutionReports.ViewModels
{
    public class CompanyCreateViewModel
    {
        [Required(ErrorMessage = "Name must me specified")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "The user must be specified")]
        [Display(Name = "Assigned User")]
        public string AssignedUser { get; set; }
        [Required(ErrorMessage = "The country must be specified")]
        public string Country { get; set; }
        [Required(ErrorMessage = "The town must be specified")]
        public string Town { get; set; }
        [Required(ErrorMessage = "The description must be specified")]
        [MaxLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Description { get; set; }
        public List<string> Countries { get; set; }

    }
}
