using Microsoft.AspNetCore.Http;
<<<<<<< HEAD
=======
using RSR.DAL.Models.TaskModel;
using RSR.DAL.Models.User;
>>>>>>> origin/master
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
<<<<<<< HEAD
=======
using System.Text.Json.Serialization;
>>>>>>> origin/master
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.TaskRes
{
    public  class TaskSubmissionResponse
    {
<<<<<<< HEAD
        public IFormFile TaskSubmission { get; set; }
=======
        public Guid TaskSubmissionId { get; set; }
        public int VersionNumber { get; set; }
        public string TaskSubmissionURL { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SubmissionStatus Status { get; set; }
        public string? StudentNotes { get; set; }
        public string StudentName { get; set; }
        public List<TaskSubmissionCommentResponse> TaskSubmissionComments { get; set; }
        public DateTime SubmittedAt { get; set; }
        public DateTime? ReviewedAt { get; set; }
>>>>>>> origin/master
    }
}
