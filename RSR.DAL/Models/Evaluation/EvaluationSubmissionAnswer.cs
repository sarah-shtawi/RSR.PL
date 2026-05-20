using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Models.Evaluation
{
    public class EvaluationSubmissionAnswer
    {
        public int Id { get; set; }

        public int EvaluationSubmissionId { get; set; }

        public EvaluationSubmission EvaluationSubmission { get; set; }

        public int EvaluationFieldId { get; set; }

        public EvaluationField EvaluationField { get; set; }

        public int Value { get; set; }
    }
}