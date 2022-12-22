﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoMVCCore.Model;

namespace DemoMVCCore.ViewModels
{
    // DTO data transfer objects
    public class HomeDetailsViewModel
    {
        public EmployeeClass Employee { get; set; }
        public string PageTitle { get; set; }
    }
}
