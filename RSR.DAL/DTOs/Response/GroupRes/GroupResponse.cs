using RSR.DAL.Models.ProjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.GroupRes
{
    public  class GroupResponse
    {
        public Guid GroupId { get; set; }
        public string GroupName {  get; set; }
        public string ProjectName { get; set; }
        public string ProjectIdea { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status ProjectStatus { get; set; }
        public string? Description { get; set; }
        public List <StudentResponse> Students { get; set; }

    }
}
