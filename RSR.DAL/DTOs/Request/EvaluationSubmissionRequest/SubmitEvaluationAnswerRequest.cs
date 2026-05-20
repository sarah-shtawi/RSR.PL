using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Request.EvaluationSubmissionRequest
{
    public class SubmitEvaluationAnswerRequest
    {
        //تمثيل اجابة واحدة فقط ولاي فورم
        public int EvaluationFieldId { get; set; }

        public int Value { get; set; }
    }
}