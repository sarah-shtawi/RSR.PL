using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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

    }
}
