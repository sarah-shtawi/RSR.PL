using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Request.EvaluationSubmissionRequest
{
    public class SubmitEvaluationRequest
    {
        //اي فورم بنجاوب وشو الاجابات الي فيه
        public int EvaluationFormId { get; set; }

        public List<SubmitEvaluationAnswerRequest> Answers { get; set; }
            = new List<SubmitEvaluationAnswerRequest>();
    }
}