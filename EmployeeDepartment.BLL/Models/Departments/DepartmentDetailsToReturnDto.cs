using System.ComponentModel.DataAnnotations;

namespace EmployeeDepartment.BLL.Models.Departments
{
    public class DepartmentDetailsToReturnDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastModificationBy { get; set; }
        public DateTime LastModificationOn { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        [Display(Name = "Creation Date")]//بغير اسمها
        public DateOnly CreationDate { get; set; }
    }
}
