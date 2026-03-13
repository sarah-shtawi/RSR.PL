using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Request.UserRequest
{
    public  class AssignExaminerRequest : AssignUserRequest
    { 
        public string ExaminerNumber { get; set; }
        public string Department { get; set; }
    }
}
