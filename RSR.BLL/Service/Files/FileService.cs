using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.Service.Files
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<string?> UploadeImageFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var imagesFolder = Path.Combine(_env.WebRootPath, "images");

                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                var filePath = Path.Combine(imagesFolder, fileName);

                try
                {
                    using (var stream = File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return fileName;
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to upload image", ex);
                }
            }
            return null;
        }


        private const long MaxFileSize = 20 * 1024 * 1024; // 20 MB
        public async Task<string?> UploadTaskFile(IFormFile TaskFile)
        {
            if(TaskFile == null || TaskFile.Length == 0)
            {
                throw new ArgumentException("Not File Uploaded Or File Is Empty");
            }
            if (TaskFile.Length > MaxFileSize)
                throw new ArgumentException("File size must not exceed 20 MB.");


            var extension = Path.GetExtension(TaskFile.FileName);

            if (!string.Equals(extension, ".pdf", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Only PDF files are allowed.");

                var TaskfileName = Guid.NewGuid().ToString() + extension;
                var TaskFolderPath = Path.Combine(_env.WebRootPath, "files", "Tasks");

                if(!Directory.Exists(TaskFolderPath))    
                   Directory.CreateDirectory(TaskFolderPath);

                  var TaskPath = Path.Combine(TaskFolderPath , TaskfileName);
                try
                {
                    using (var stream = new FileStream(TaskPath, FileMode.Create))
                    {
                        await TaskFile.CopyToAsync(stream);
                    }
                    return TaskfileName;
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to upload Task File", ex);
                }  
        }

        public async Task<string?> UploadThesisFile(IFormFile ThesisFile)
        {
            if (ThesisFile == null || ThesisFile.Length == 0)
            {
                throw new ArgumentException("Not File Uploaded Or File Is Empty");
            }
            if (ThesisFile.Length > MaxFileSize)
                throw new ArgumentException("File size must not exceed 20 MB.");


            var extension = Path.GetExtension(ThesisFile.FileName);

            if (!string.Equals(extension, ".pdf", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Only PDF files are allowed.");


            var ThesisfileName = Guid.NewGuid().ToString() + extension;
            var ThesisFolderPath = Path.Combine(_env.WebRootPath, "files", "Thesis");

            if (!Directory.Exists(ThesisFolderPath))
                Directory.CreateDirectory(ThesisFolderPath);

            var ThesisPath = Path.Combine(ThesisFolderPath, ThesisfileName);
            try
            {
                using (var stream = new FileStream(ThesisPath, FileMode.Create))
                {
                    await ThesisFile.CopyToAsync(stream);
                }
                return ThesisfileName;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to upload Thesis File", ex);
            }
        }





    }
}
