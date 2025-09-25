    using System.ComponentModel.DataAnnotations;

    namespace EmployeeDepartment.BLL.Models.Departments
    {
        public class CreatedDepartmentDto
        {
            [Required(ErrorMessage = "Code is required.")]
            public string Code { get; set; } = null!;
            [Required(ErrorMessage = "Name is required.")]
            public string Name { get; set; } = null!;
            public string? Description { get; set; }
            [Display(Name="Date Of Creation ")]
            public DateOnly CreationDate { get; set; }
        }
    }
