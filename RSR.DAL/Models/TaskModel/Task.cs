using RSR.DAL.Models.ProjectGroupModel;
using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Models.TaskModel
{
    public  class Task
    {
        public Guid TaskId { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; }
        public string? SupervisorNotes { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime DeadLine { get; set; }
        public string? TaskFileURL { get; set; }

        // Relation With Group 
        public Group Group { get; set; }
        public Guid GroupId { get; set; }

        // Relation with Supervisor 
        public SupervisorProfile Supervisor {  get; set; }
        public string SupervisorId { get; set; }

        // relation with TaskSubmission 
        public List<TaskSubmission> TaskSubmissions { get; set; } = new();

    }
}
