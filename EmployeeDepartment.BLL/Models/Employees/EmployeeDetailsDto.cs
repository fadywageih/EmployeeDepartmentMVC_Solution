using EmployeeDepartment.DAL.Common.Enums;
using EmployeeDepartment.DAL.Models.Departments;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDepartment.BLL.Models.Employees
{
    public class EmployeeDetailsDto
    {

        public int PhoneNumber { get; set; }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public DateTime HiringDate { get; set; }    
        public Gender Gender { get; set; } 
        public EmployeeType EmployeeType { get; set; }
        #region Adminstration
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string? Address { get; set; }
        public string Department { get; set; }
        public int? DepartmentId { get; set; }
        public string? Image { get; set; }
        #endregion
    }
}
