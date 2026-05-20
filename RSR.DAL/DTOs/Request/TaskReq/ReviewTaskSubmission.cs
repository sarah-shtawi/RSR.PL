using RSR.DAL.Models.TaskModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Request.TaskReq
{
    public  class ReviewTaskSubmission
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SubmissionStatus status {  get; set; }
        public string? Comment { get; set; }



    }
}
