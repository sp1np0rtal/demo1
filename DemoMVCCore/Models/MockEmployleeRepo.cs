using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMVCCore.Model
{
    public class MockEmployleeRepo : IEmployeeRepo
    {
        public List<EmployeeClass> _empList;

        public MockEmployleeRepo()
        {
            _empList = new List<EmployeeClass>()
            {
                new EmployeeClass() { Id = 1, Name = "Mary", Dept = Dept.HR, Email = "mary@pr.com"},
                new EmployeeClass() { Id = 2, Name = "John", Dept = Dept.IT, Email = "john@pr.com"},
                new EmployeeClass() { Id = 3, Name = "Sam", Dept = Dept.MO, Email = "san@pr.com"}
            };
        }

        public EmployeeClass GetEmployee(int id)
        {
            return _empList.FirstOrDefault(c => c.Id == id);
        }
        
        public IEnumerable<EmployeeClass> GetAllEmployees()
        {
            return _empList;
        }

        public EmployeeClass Add(EmployeeClass emp)
        {
            emp.Id = _empList.Max(e => e.Id) + 1;
            _empList.Add(emp);
            return emp;
        }

        public EmployeeClass Update(EmployeeClass e)
        {
            //throw new Exception(new NotImplementedException());
            EmployeeClass emp = _empList.FirstOrDefault(r => r.Id == e.Id);
            if (emp != null)
            {
                emp.age = e.age;
                emp.Dept = e.Dept;
                emp.Name = e.Name;
                emp.Email = e.Email;
            }
            return emp;
        }

        public EmployeeClass Delete(int id)
        {
            //throw new Exception(new NotImplementedException());
            EmployeeClass emp = _empList.FirstOrDefault(r => r.Id == id);
            if (emp != null)
            {
                _empList.Remove(emp);
            }
            return emp;
        }
    }
}
