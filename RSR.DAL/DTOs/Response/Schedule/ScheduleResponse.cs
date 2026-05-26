using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.Schedule
{
    public  class ScheduleResponse
    {
        public Guid ScheduleId { get; set; }
        public string GroupName { get; set; }
        public string SupervisorName { get; set; }


        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string? Notes { get; set; }
        public string ProjectName { get; set; }
        public string ThesisURL { get; set; }


        public List<string> Students { get; set; }
        public List<string> Examiners { get; set; }

    }
}
