using RSR.DAL.DTOs.Response;
using RSR.DAL.DTOs.Response.EvaluationResponse;

namespace RSR.BLL.Service.EvaluationService
{
    public interface IFinalEvaluationResultService
    {
        // =========================
        // GENERATE FINAL RESULT
        // =========================
        Task<FinalEvaluationResultResponse>
            GenerateAsync(Guid groupId);

        // =========================
        // PUBLISH RESULT
        // =========================
        Task<bool>
            PublishAsync(Guid id);

        // =========================
        // SET RESULT AS DRAFT
        // =========================
        Task<bool>
            SetDraftAsync(Guid id);

        // =========================
        // GET PUBLISHED RESULT
        // =========================
        Task<FinalEvaluationResultResponse>
         
            GetPublishedResultAsync(Guid groupId);
        //final grade to student

        Task<StudentFinalGradeResponse>
        GetStudentFinalGradeAsync(Guid groupId);

        //final grade to supervisor

        Task<List<SupervisorFinalGradeResponse>>
       GetSupervisorGroupsFinalGradesAsync(string supervisorId);

        //Final Groups Details
        Task<List<CoordinatorFinalResultResponse>>
           GetAllFinalResultsAsync();
    }
}