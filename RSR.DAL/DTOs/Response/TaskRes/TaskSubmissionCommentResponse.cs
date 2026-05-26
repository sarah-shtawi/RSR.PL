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
        public bool Success { get; set; }
        public string message { get; set; }
        public Guid TaskSubmissionCommentId { get; set; }
        public Guid? ParentCommentId { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public string role { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
