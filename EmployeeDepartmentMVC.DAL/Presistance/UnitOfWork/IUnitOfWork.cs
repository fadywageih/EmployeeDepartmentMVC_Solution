//done code
using EmployeeDepartment.DAL.Presistance.Repositories.Departments;
using EmployeeDepartment.DAL.Presistance.Repositories.Employees;

namespace EmployeeDepartment.DAL.Presistance.UnitOfWork
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        public IEmployeeRepository EmployeeRepository { get; }
        public IDepartmentRepository DepartmentRepository { get; }
        Task<int> CompleteAsync();
    }
}
