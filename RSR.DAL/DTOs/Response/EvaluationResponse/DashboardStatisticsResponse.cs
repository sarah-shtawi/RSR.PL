using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.EvaluationResponse
{
    public class DashboardStatisticsResponse
    {
         // FORMS
         public int TotalForms { get; set; }

        public int PublishedForms { get; set; }

         //SUBMISSIONS
         public int TotalEvaluations { get; set; }
    }
}