using EmployeeDepartment.DAL.Models.Identity;

namespace EmployeeDepartment.BLL.Common.Services.EmailSettings
{
    public interface IEmailSettings
    {
        public void SendEmail(Email email);
    }
}
