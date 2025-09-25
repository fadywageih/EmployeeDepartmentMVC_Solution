using EmployeeDepartment.DAL.Models.Departments;
using EmployeeDepartment.DAL.Presistance.Data;
using EmployeeDepartment.DAL.Presistance.Repositories.Generic;

namespace EmployeeDepartment.DAL.Presistance.Repositories.Departments
{
    public class DepartmentRepository :GenericRepository<Department>, IDepartmentRepository
    {
       public DepartmentRepository(ApplicationDBContext dbContext) : base(dbContext)
        {

        }
    }
}
