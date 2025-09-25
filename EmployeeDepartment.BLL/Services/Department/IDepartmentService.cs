//Done code
using EmployeeDepartment.BLL.Models.Departments;

namespace EmployeeDepartment.BLL.Services.Department
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentToReturnDto>> GetAllDepartmentsAsync();
        Task<DepartmentDetailsToReturnDto?> GetDepartmentByIdAsync(int id);
        Task<int> CreateDepartmentAsync(CreatedDepartmentDto departmentDto);
        Task<int> UpdateDepartmentAsync(UpdateDepartmentDto department);
        Task<bool> DeletedDepartmentAsync(int id);
    }
}
