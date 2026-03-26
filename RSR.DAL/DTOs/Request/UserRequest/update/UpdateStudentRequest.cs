using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Request.UserRequest.update
{
    public  class UpdateStudentRequest : UpdateUserRequest
    {
        public string StudentNumber { get; set; }
        public string College { get; set; }
        public string Major { get; set; }
    }
}
