using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DemoMVCCore.Model;
using Microsoft.AspNetCore.Http;

namespace DemoMVCCore.ViewModels
{
    public class EmployeeCreateViewModel
    {
        
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }
        [RegularExpression(@"[0-9]*", ErrorMessage = "Invalid Age")]
        [Display(Name = "Age")]
        public string age { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Department")]
        public Dept? Dept { get; set; }
        public IFormFile Photo { get; set; } // Filename ==> to object
        public string SSI { get; set; }

    }
}
