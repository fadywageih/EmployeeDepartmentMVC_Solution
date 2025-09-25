using EmployeeDepartment.DAL.Models.Employees;

namespace EmployeeDepartment.DAL.Models.Departments
{
    public class Department:ModelBase
    {

        public string Name { get; set; }=null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly CreationDate { get; set; }
        #region Employee
        //Navigation Property [Many]
        public virtual ICollection<Employee> Employees { get; set; } =new HashSet<Employee>();
        #endregion

    }
}
