using Microsoft.AspNetCore.Http;
using RSR.DAL.Models.TaskModel;
using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.TaskRes
{
    public  class TaskSubmissionResponse
    {
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
    }
}
