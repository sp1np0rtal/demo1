using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DemoMVCCore.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DemoMVCCore.DataAccess.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { 
        }

        public DbSet<EmployeeClass> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();

            foreach(var fk in modelBuilder.Model.GetEntityTypes())
            {
                foreach(var fk2 in fk.GetForeignKeys())
                {
                    fk2.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }
        }
    }
}
