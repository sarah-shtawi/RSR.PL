using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.EvaluationSubmissionResponse
{
    public class SubmitEvaluationAnswerResponse 
    {
        public int EvaluationFieldId { get; set; }

        public int Value { get; set; }
    }
}