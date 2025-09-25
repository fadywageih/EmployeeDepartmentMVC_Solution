using EmployeeDepartment.BLL.Models.Departments;
using EmployeeDepartment.DAL.Presistance.Repositories.Departments;
using EmployeeDepartment.DAL.Presistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDepartment.BLL.Services.Department
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<DepartmentToReturnDto>> GetAllDepartmentsAsync()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsQuerable().
                Select(D => new DepartmentToReturnDto
            {
                Id = D.Id,
                Name = D.Name,
                Code = D.Code,
                CreationDate = D.CreationDate,
            }).AsNoTracking().ToListAsync();
            return departments ;

        }
        public async Task<DepartmentDetailsToReturnDto?> GetDepartmentByIdAsync(int id)
        {
            var department=await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
            if(department is { })
            { 
                 return new DepartmentDetailsToReturnDto
                 {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreationDate = department.CreationDate,
                CreatedBy = department.CreatedBy,
                CreatedOn = department.CreatedOn,
                LastModificationBy = department.LastModificationBy,
                LastModificationOn = department.LastModificationOn,
            };
            }
            return null;
        }
        public async Task<int> CreateDepartmentAsync(CreatedDepartmentDto departmentDto)
        {
            var Createdepartment = new DAL.Models.Departments.Department()
            {
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                CreatedBy = 1,
                LastModificationBy = 1,
                LastModificationOn = DateTime.UtcNow,
            };
              _unitOfWork.DepartmentRepository.Add(Createdepartment);
            return await _unitOfWork.CompleteAsync();
        }
        public async Task<int> UpdateDepartmentAsync(UpdateDepartmentDto department)
        {
            var existingDepartment = await _unitOfWork.DepartmentRepository.GetByIdAsync(department.Id);
            if (existingDepartment == null)
            {
                // لو ملقينهوش، ممكن ترمي Exception أو ترجع 0
                return 0; // أو throw new KeyNotFoundException($"Department with ID {department.Id} not found.");
            }
            existingDepartment.Name = department.Name;
            existingDepartment.Code = department.Code;
            existingDepartment.Description = department.Description;
            existingDepartment.CreationDate = department.CreationDate;
            existingDepartment.LastModificationBy = 1; // أو خدّها من الـ User في الـ Session
            existingDepartment.LastModificationOn = DateTime.UtcNow;

            // 👇 الخطوة 3: استخدم Update على الكائن الموجود (مش كائن جديد)
            _unitOfWork.DepartmentRepository.Update(existingDepartment);
            return await _unitOfWork.CompleteAsync();
        }
        public  async Task<bool> DeletedDepartmentAsync(int id)
        {
            var DepartmentRepo =  _unitOfWork.DepartmentRepository;
            var department = await DepartmentRepo.GetByIdAsync(id);
            if (department is not null)
            {

                DepartmentRepo.Delete(department);
              
            } 
            return await _unitOfWork.CompleteAsync()>0;
        }

    }
}
