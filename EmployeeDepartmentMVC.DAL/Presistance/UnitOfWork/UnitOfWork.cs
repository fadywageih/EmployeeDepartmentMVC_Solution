//done code
using EmployeeDepartment.DAL.Presistance.Data;
using EmployeeDepartment.DAL.Presistance.Repositories.Departments;
using EmployeeDepartment.DAL.Presistance.Repositories.Employees;

namespace EmployeeDepartment.DAL.Presistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public IEmployeeRepository EmployeeRepository {get
            { return new EmployeeRepository(_applicationDBContext); } }

        public IDepartmentRepository DepartmentRepository { get 
            {
                return new DepartmentRepository(_applicationDBContext);
            } }
        public UnitOfWork (ApplicationDBContext applicationDBContext) 
        {
            _applicationDBContext = applicationDBContext;
        }

        public async Task<int> CompleteAsync()
        {
           return await _applicationDBContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _applicationDBContext.DisposeAsync();
        }
    }
}
