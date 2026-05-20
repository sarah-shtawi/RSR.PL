using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Request.TaskReq
{
    public  class TaskSubmissionRequest
    {
        public IFormFile TaskSubmission {  get; set; }
        public string? StudentNotes { get; set; }
    }
}
