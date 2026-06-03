using RSR.DAL.Models.ProjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.Dashboared
{
    public  class StudentDashboard
    {
        public int TotalTask {  get; set; }
        public int CompletedTask { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status ProjectStatus { get; set; }

    }
}
