using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.User
{
    public  class StudentGetResponse : GetUserResponse
    {
        public string StudentNumber { get; set; }
        public string College { get; set; }
        public string Major { get; set; }
        public string? PictureProfileURL { get; set; }

    }
}
