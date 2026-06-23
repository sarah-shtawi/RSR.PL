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
          Guid groupId,
          string userId);


        Task<List<EvaluationSubmission>>
            GetGroupSubmissionsAsync(Guid groupId);

        Task<int> CountAsync();
        Task<List<EvaluationSubmission>> GetUserSubmissionsByFormAsync(int formId, string userId);
        Task<EvaluationSubmission?> GetUserSubmissionAsync(int formId, string userId);
    }
}