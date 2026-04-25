using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.Service.Files
{
    public  interface IFileService
    {
        Task<string?> UploadeImageFile(IFormFile file);
        Task<string?> UploadTaskFile(IFormFile TaskFile);
    }
}
