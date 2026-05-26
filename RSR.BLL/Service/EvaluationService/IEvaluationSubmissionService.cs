using RSR.DAL.DTOs.Request.EvaluationSubmissionRequest;
using RSR.DAL.DTOs.Response.EvaluationResponse;
using RSR.DAL.DTOs.Response.EvaluationSubmissionResponse;

namespace RSR.BLL.Service.EvaluationService
{
    public interface IEvaluationSubmissionService
    {
        Task<SubmitEvaluationResponse?>
            SubmitAsync(
                SubmitEvaluationRequest request);

        Task<List<CreateEvaluationFormResponse>>
            GetMyFormsAsync();

        // FIXED HERE
        Task<FinalGradeResponse>
            GetFinalGradeAsync(Guid groupId);
    }
}