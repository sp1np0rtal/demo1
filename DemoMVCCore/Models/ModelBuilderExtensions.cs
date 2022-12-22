using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DemoMVCCore.Model
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder mdl)
        {
            mdl.Entity<EmployeeClass>().HasData(
            new EmployeeClass
            {
                Id = 1,
                Name = "Mary",
                age = "22",
                Dept = Dept.HR,
                Email = "mary@goo.com"
            },
            new EmployeeClass
            {
                Id = 2,
                Name = "Fred",
                age = "23",
                Dept = Dept.IT,
                Email = "fred@goo.com"
            }

            );

        }
    }
}
