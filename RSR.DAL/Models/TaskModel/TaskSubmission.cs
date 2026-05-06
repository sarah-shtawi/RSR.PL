using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Models.TaskModel
{
    public enum SubmissionStatus
    {
        Submitted = 1  , Approved = 2  , Rejected = 3  
    }
    public  class TaskSubmission
    {
        public Guid TaskSubmissionId { get; set; } = Guid.NewGuid();
        public string SubmissionTaskFileURL { get; set; }
        public SubmissionStatus Status { get; set; }
        public string? StudentNotes { get; set; }

        // relation with student 
        public StudentProfile Student { get; set; }
        public string StudentId { get; set; }
        public Guid GroupId { get; set; }


        // relation with Task
        public Task Task { get; set; }
        public Guid TaskId { get; set; }

        // relation with TaskSubmissionComments
        public List<TaskSubmissionComment> TaskSubmissionComments { get; set; }

        public int VersionNumber { get; set; }
        public bool IsLatest { get; set; }


        public DateTime SubmittedAt { get; set; }
        public DateTime? ReviewedAt { get; set; }

    }
}
