using EmployeeDepartment.DAL.Models.Employees;
using EmployeeDepartment.DAL.Presistance.Data;
using EmployeeDepartment.DAL.Presistance.Repositories.Generic;

namespace EmployeeDepartment.DAL.Presistance.Repositories.Employees
{
    public class EmployeeRepository :GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDBContext dbContext) : base(dbContext)
        {

        }
    }
}
