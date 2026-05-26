using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.EvaluationResponse
{
    public class FinalEvaluationResultResponse
    {
        public int Id { get; set; }

        public Guid GroupId { get; set; }

        public double SupervisorGrade { get; set; }

        public double ExaminerGrade { get; set; }

        public double FinalGrade { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? PublishedAt { get; set; }
    }
}