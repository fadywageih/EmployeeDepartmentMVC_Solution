using EmployeeDepartment.DAL.Common.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDepartment.BLL.Models.Employees
{
    public class CreatedEmployeeDto
    {
        [MaxLength(50, ErrorMessage = "Max Length of name is 50 chars")]
        [MinLength(5, ErrorMessage = "Min Length of name is 5 chars")]
        public string Name { get; set; } = null!;
        [Range(18, 39)]
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public int PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        [Required]
        public string Email { get; set; } = null!;
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
        public int? Code { get; set; }
        public IFormFile? Image { get; set; }
    }
}
