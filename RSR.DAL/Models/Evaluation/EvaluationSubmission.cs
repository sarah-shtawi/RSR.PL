using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSR.DAL.Models.User;

namespace RSR.DAL.Models.Evaluation
{
    public class EvaluationSubmission
    {
        public int Id { get; set; }

        public int EvaluationFormId { get; set; }

        public EvaluationForm EvaluationForm { get; set; }

        public DateTime SubmittedAt { get; set; }

        public ICollection<EvaluationSubmissionAnswer> Answers { get; set; }
            = new List<EvaluationSubmissionAnswer>();


        public string? SubmittedByUserId { get; set; }

        public ApplicationUser? SubmittedByUser { get; set; }
    }
}