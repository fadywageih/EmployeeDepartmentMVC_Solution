using Microsoft.AspNetCore.Http;

namespace EmployeeDepartment.BLL.Common.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly List<string> _allowedExtensions = new List<string> { ".jpg", ".png", ".pdf", ".docx" };
        private const int _maxFileSizeInBytes = 3 * 1024 * 1024; // 3 MB

        public string UploadFile(IFormFile file, string FolderName)
        {
        #region Validations
            var fileExtension = Path.GetExtension(file.FileName);
            if (!_allowedExtensions.Contains(fileExtension ))
            {
                throw new Exception("File type is not allowed.");
            };
            if (file.Length > _maxFileSizeInBytes)
            {
                throw new Exception("File size exceeds the maximum limit of 3 MB.");
            } 
            //Get Located Folder path;
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files",FolderName );
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
            //Get File Name and make it unique;
            var filename=$"{Guid.NewGuid()}{fileExtension}";
            //3 get full path to file;
            var filePath = Path.Combine(FolderPath, filename);
            //4 save file to path;[data per time]
            using var filestream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(filestream);
            return filename;
            #endregion
        }
        public bool Delete(string filePath)
        {
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }

    }
}
