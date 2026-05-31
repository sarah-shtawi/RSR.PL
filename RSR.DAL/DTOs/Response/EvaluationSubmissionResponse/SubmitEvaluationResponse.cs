using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.EvaluationSubmissionResponse
{
    public class SubmitEvaluationResponse : BaseResponse
    {
        //النتيجة الكاملة بعد إرسال التقييم

        //رقم الـ submission
        public int SubmissionId { get; set; }

        //Form id
        public int EvaluationFormId { get; set; }
        //وقت الإرسال
        public DateTime SubmittedAt { get; set; }
        //الاجابات
        public List<SubmitEvaluationAnswerResponse> Answers { get; set; }
            = new List<SubmitEvaluationAnswerResponse>();
    }
}