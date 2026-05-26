using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.EvaluationSubmissionResponse
{
    public class FinalGradeResponse
    {
        public Guid GroupId { get; set; }

        public double SupervisorGrade { get; set; }

        public double ExaminerGrade { get; set; }

        public double FinalGrade { get; set; }
    }
}