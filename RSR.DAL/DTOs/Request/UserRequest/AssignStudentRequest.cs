using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RSR.DAL.DTOs.Request.UserRequest
{
    public  class AssignStudentRequest : AssignUserRequest
    {
      
        public string StudentNumber { get; set; }

        public string? College { get; set; } 
        public string? Major { get; set; }

    }
}
