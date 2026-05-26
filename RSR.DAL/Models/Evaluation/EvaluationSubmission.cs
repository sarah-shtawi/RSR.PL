using RSR.DAL.Models.ProjectGroupModel;
using RSR.DAL.Models.User;

namespace RSR.DAL.Models.Evaluation
{
    public class EvaluationSubmission
    {
        public int Id { get; set; }

        // =========================
        // FORM
        // =========================
        public int EvaluationFormId { get; set; }

        public EvaluationForm EvaluationForm { get; set; }

        // =========================
        // GROUP
        // =========================
        public Guid GroupId { get; set; }

        public Group Group { get; set; }

        // =========================
        // USER
        // =========================
        public string? SubmittedByUserId { get; set; }

        public ApplicationUser? SubmittedByUser { get; set; }

        // =========================
        // SCORE
        // =========================
        public float TotalScore { get; set; }

        // =========================
        // DATE
        // =========================
        public DateTime SubmittedAt { get; set; }

        // =========================
        // ANSWERS
        // =========================
        public ICollection<EvaluationSubmissionAnswer> Answers
        { get; set; }
            = new List<EvaluationSubmissionAnswer>();
    }
}