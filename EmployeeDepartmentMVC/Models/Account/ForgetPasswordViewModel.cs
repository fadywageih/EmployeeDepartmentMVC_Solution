using System.ComponentModel.DataAnnotations;

namespace EmployeeDepartmentMVC.Models.Account
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage ="Email is Required")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }
    }
}
