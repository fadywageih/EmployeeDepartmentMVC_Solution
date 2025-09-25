using EmployeeDepartment.DAL.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDepartment.BLL.Models.Employees
{
    public class UpdatedEmployeeDto
    {
        public int Id { get; set; }
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
        [Required]
        public string? Email { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public int? DepartmentId { get; set; }
        public string? Image { get; set; }
    }
}
