using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMVCCore.Model
{
    public interface IEmployeeRepo
    {
        EmployeeClass GetEmployee(int id);
        IEnumerable<EmployeeClass> GetAllEmployees();
        EmployeeClass Add(EmployeeClass emp);
        EmployeeClass Update(EmployeeClass empU);
        EmployeeClass Delete(int id);

    }
}
