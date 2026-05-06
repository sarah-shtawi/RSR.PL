using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Models.TaskModel
{
    public  class TaskSubmissionComment
    {
        public Guid TaskSubmissionCommentId { get; set; } = Guid.NewGuid();

        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }


        // relation with TaskSubmission 
        public Guid TaskSubmissionId { get; set; }
        public TaskSubmission TaskSubmission { get; set; }

        // relation with User [ Student or Supervisor ] 
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        // self Relation 
        public Guid? ParentCommentId {  get; set; } 
        public TaskSubmissionComment ParentComment { get; set; }
        public List<TaskSubmissionComment> Replies { get; set; } = new List<TaskSubmissionComment>();

    }
}
