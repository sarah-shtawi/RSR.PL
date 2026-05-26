using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSR.DAL.Models.ProjectGroupModel;

namespace RSR.DAL.Models.Evaluation
{
    public class FinalEvaluationResult
    {
        public int Id { get; set; }

        // =========================
        // GROUP
        // =========================
        public Guid GroupId { get; set; }

        public Group Group { get; set; }

        // =========================
        // GRADES
        // =========================
        public double SupervisorGrade { get; set; }

        public double ExaminerGrade { get; set; }

        public double FinalGrade { get; set; }

        // =========================
        // STATUS
        // =========================
        public string Status { get; set; } = "Draft";

        // =========================
        // DATES
        // =========================
        public DateTime CreatedAt { get; set; }

        public DateTime? PublishedAt { get; set; }
    }
}