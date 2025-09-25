using EmployeeDepartment.BLL.Common.Services;
using EmployeeDepartment.BLL.Models.Employees;
using EmployeeDepartment.DAL.Presistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;


namespace EmployeeDepartment.BLL.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentService _attachmentService;

        public EmployeeService(IUnitOfWork unitOfWork,IAttachmentService attachmentService) 
        {

            _unitOfWork = unitOfWork;
            _attachmentService = attachmentService;
        }
        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(string search)
        {
            return await _unitOfWork.EmployeeRepository.GetAllAsQuerable()
                .Where(e => !e.IsDeleted && (string.IsNullOrEmpty(search) 
                || e.Name.ToLower().Contains(search.ToLower())))
                .Include(e => e.Department)
                .Select(Employee => new EmployeeDto()
                {
                    Id = Employee.Id,
                    Name = Employee.Name,
                    Age = Employee.Age,
                    Salary = Employee.Salary,
                    IsActive = Employee.IsActive,
                    Email = Employee.Email,
                    Gender = Employee.Gender.ToString(),
                    EmployeeType = Employee.EmployeeType.ToString(),
                    Department = Employee.Department.Code
                }).ToListAsync();
        }
        public async Task<EmployeeDetailsDto?> GetEmployeeByIdAsync(int id)
        {
            var employeeDto = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
            if (employeeDto is { })
                return new EmployeeDetailsDto()
                {
                    Id = employeeDto.Id,
                    Name = employeeDto.Name,    
                    Age = employeeDto.Age,
                    Address = employeeDto.Address,
                    Salary = employeeDto.Salary,
                    IsActive = employeeDto.IsActive,
                    Email = employeeDto.Email,
                    PhoneNumber = employeeDto.PhoneNumber,
                    HiringDate = employeeDto.HiringDate,
                    Gender = employeeDto.Gender,
                    EmployeeType = employeeDto.EmployeeType,
                    Department = employeeDto.Department?.Name ?? "No Department",
                    Image = employeeDto.Image,

                };
            return null;
        }
        public async Task<int> CreateEmployeeAsync(CreatedEmployeeDto employeeDto)
        {
            var employee = new EmployeeDepartment.DAL.Models.Employees.Employee()
            {
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                Salary = employeeDto.Salary,
                IsActive = employeeDto.IsActive,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                CreatedBy = 1,
                LastModificationBy = 1,
                LastModificationOn = DateTime.UtcNow
            };
            if(employeeDto.Image is not null)
            {
                employee.Image = _attachmentService.UploadFile(employeeDto.Image, "Images");
            }
                _unitOfWork.EmployeeRepository.Add(employee);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> UpdateEmployeeAsync(UpdatedEmployeeDto employeeDto)
        {
            var employeeUpdate = new DAL.Models.Employees.Employee()
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                Salary = employeeDto.Salary,
                IsActive = employeeDto.IsActive,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                CreatedBy = 1,
                LastModificationBy = 1,
                LastModificationOn = DateTime.UtcNow

            };
             _unitOfWork.EmployeeRepository.Update(employeeUpdate);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employeeRepo = _unitOfWork.EmployeeRepository;
            var employee = await employeeRepo.GetByIdAsync(id);
            if(employee is { })
            {
                employeeRepo.Delete(employee);
            }
            return await _unitOfWork.CompleteAsync()>0;
        }
    }
}
