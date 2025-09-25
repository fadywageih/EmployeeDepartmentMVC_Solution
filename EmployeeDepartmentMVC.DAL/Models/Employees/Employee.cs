using EmployeeDepartment.DAL.Common.Enums;
using EmployeeDepartment.DAL.Models.Departments;

namespace EmployeeDepartment.DAL.Models.Employees
{
    public class Employee:ModelBase
    {
        public string Name { get; set; }=null!;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public Gender Gender { get; set; } 
        public EmployeeType EmployeeType { get; set; }
        #region Department
        public int? DepartmentId { get; set; }
        //Navigation Property [one]
        public virtual Department? Department { get; set; }
        #endregion
        #region Attact
        public string? Image { get; set; }
        #endregion

    }
}
