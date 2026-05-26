using RSR.DAL.Models.Evaluation;

namespace RSR.DAL.Repository.EvaluationRepository
{
    public interface IEvaluationSubmissionRepository
    {
        Task<EvaluationSubmission>
            CreateAsync(EvaluationSubmission submission);

        Task<bool>
            HasUserSubmittedAsync(
                int formId,
                string userId);

        Task<List<EvaluationSubmission>>
            GetGroupSubmissionsAsync(Guid groupId);

        Task<int> CountAsync();
    }
}