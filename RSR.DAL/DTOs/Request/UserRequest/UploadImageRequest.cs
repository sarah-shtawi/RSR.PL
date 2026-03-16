using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Request.UserRequest
{
    public  class UploadImageRequest
    {
        public IFormFile MainImage { get; set; }
    }
}
