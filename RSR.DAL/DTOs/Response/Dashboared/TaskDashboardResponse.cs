using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.Dashboared
{
    public  class TaskDashboardResponse
    {
        public Guid TaskSubmissionId { get; set; }
        public string title { get; set; }
        public string GroupName { get; set; }
        public string StudentName { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
