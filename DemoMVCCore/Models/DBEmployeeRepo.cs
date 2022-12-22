using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoMVCCore.DataAccess.Models;
using Microsoft.Extensions.Logging;

namespace DemoMVCCore.Model
{
    public class DBEmployeeRepo : IEmployeeRepo
    {
        private readonly AppDbContext context;
        private readonly ILogger<DBEmployeeRepo> _logger;
    
        public DBEmployeeRepo(AppDbContext context,
            ILogger<DBEmployeeRepo> logger)
        {
            this.context = context;
            _logger = logger;
        }

        public EmployeeClass GetEmployee(int id)
        {
            _logger.LogTrace("DBTrace");
            _logger.LogDebug("DBDebug");
            _logger.LogInformation("DBInfo");
            _logger.LogWarning("DBWarn");
            _logger.LogError("DBError");
            _logger.LogCritical("DBCritical");

            return context.Employees.Find(id);
        }

        public IEnumerable<EmployeeClass> GetAllEmployees()
        {
            //throw new NotImplementedException(); 
            return context.Employees;
            //return null;
        }

        public EmployeeClass Add(EmployeeClass emp)
        {
            context.Employees.Add(emp);
            context.SaveChanges();
            return emp;
        }
        public EmployeeClass Update(EmployeeClass e)
        {
            var employ = context.Employees.Attach(e);
            employ.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            //throw new NotImplementedException();
            return e;
        }

        public EmployeeClass Delete(int id)
        {
            EmployeeClass emp=   context.Employees.Find(id);
            if (emp != null)
            {
                context.Employees.Remove(emp);
                context.SaveChanges();
            }
            //throw new NotImplementedException();
            return emp;
        }

    }
}
