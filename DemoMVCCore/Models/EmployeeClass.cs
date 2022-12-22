using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DemoMVCCore.Model
{
    public class EmployeeClass
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage ="Name cannot exceed 50 characters")]
        public string Name {get;set;}
        [RegularExpression(@"[0-9]*", ErrorMessage = "Invalid Age")]
        [Display(Name="Age")]
        public string age { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Format") ]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Department")]
        public Dept? Dept { get; set; }
        public string PhotoPath { get; set; }
        public string SSI { get; set; }

    }
}
