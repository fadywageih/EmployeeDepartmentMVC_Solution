//Done code
using EmployeeDepartment.DAL.Models.Identity;
using System.Net;
using System.Net.Mail;

namespace EmployeeDepartment.BLL.Common.Services.EmailSettings
{
    public class EmailSettings : IEmailSettings
    {
        public void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            //Sender -Reciver
            //
            Client.Credentials = new NetworkCredential("fadywageih14@gmail.com", "mmairvtxklpeyrms");
            Client.Send("fadywageih14@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
