using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.Dashboared
{
    public  class SupervisorDashboard
    {
        public int MyGroups { get; set; }

        public int TotalPendingReviews { get; set; }

        public int ThesisPending { get; set; }

        public int TaskSubmissionsPending { get; set; }
    }
}
