using EmployeeDepartment.DAL.Models.Employees;
using EmployeeDepartment.DAL.Presistance.Repositories.Generic;

namespace EmployeeDepartment.DAL.Presistance.Repositories.Employees
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {

    }
}
