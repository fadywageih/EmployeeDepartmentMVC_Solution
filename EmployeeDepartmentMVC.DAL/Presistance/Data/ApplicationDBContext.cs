using EmployeeDepartment.DAL.Models.Departments;
using EmployeeDepartment.DAL.Models.Employees;
using EmployeeDepartment.DAL.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EmployeeDepartment.DAL.Presistance.Data
{
    public class ApplicationDBContext:IdentityDbContext<ApplicationUser>

    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        #region DbSet
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        #endregion
    }
}
