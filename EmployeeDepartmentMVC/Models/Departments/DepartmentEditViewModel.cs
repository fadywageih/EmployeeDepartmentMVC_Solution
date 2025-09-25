using System.ComponentModel.DataAnnotations;

namespace EmployeeDepartmentMVC.Models.Departments
{
    public class DepartmentEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly CreationDate { get; set; }
    }
}
