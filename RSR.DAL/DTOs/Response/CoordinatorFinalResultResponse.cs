using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response
{
    public class CoordinatorFinalResultResponse
    {
        public Guid GroupId { get; set; }

        public string GroupName { get; set; }

        public string SupervisorName { get; set; }

        public double SupervisorGrade { get; set; }

        public double ExaminerGrade { get; set; }

        public double FinalGrade { get; set; }

        public string Status { get; set; }
    }
}