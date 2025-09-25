using Microsoft.AspNetCore.Http;

namespace EmployeeDepartment.BLL.Common.Services
{
    public interface IAttachmentService
    {
        string UploadFile(IFormFile file,string FolderName);
        bool Delete(string filePath);
    }
}
