using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.TaskRes
{
    public class TaskDetailsResponse :BaseResponse
    {
        public Guid TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? SupervisorNotes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeadLine { get; set; }
        public string? TaskFileURL { get; set; }
        public string SupervisorName { get; set; }
        public List<TaskSubmissionResponse> TaskSubmissions { get; set; }

    }
}
