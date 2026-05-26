using System.ComponentModel.DataAnnotations;

namespace RSR.DAL.DTOs.Request.EvaluationSubmissionRequest
{
    public class SubmitEvaluationRequest
    {
        [Required]
        public int EvaluationFormId { get; set; }

        [Required]
        public Guid GroupId { get; set; }

        [Required]
        public List<SubmitEvaluationAnswerRequest>
            Answers
        { get; set; }
            = new();
    }
}