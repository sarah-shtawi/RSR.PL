using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.TaskRes
{
    public  class TaskSubmissionResponse
    {
        public IFormFile TaskSubmission { get; set; }
    }
}
