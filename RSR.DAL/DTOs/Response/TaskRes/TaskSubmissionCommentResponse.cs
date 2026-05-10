using RSR.DAL.Models.TaskModel;
using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.TaskRes
{
    public  class TaskSubmissionCommentResponse
    {
        public Guid TaskSubmissionCommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }
    }
}
