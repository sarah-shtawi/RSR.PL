using RSR.DAL.DTOs.Request.EvaluationRequest;
using RSR.DAL.DTOs.Response.EvaluationResponse;

namespace RSR.BLL.Services.EvaluationService
{
    public interface IEvaluationFormService
    {
        Task<CreateEvaluationFormResponse> CreateAsync(CreateEvaluationFormRequest request);
        Task<CreateEvaluationFormResponse> GetByIdAsync(int id);
        Task<bool> PublishAsync(int id);
        Task<bool> SetDraftAsync(int id);
        Task<bool> ArchiveAsync(int id);
        Task<UpdateEvaluationFormResponse?> UpdateAsync(int id, UpdateEvaluationFormRequest request);
        Task<List<CreateEvaluationFormResponse>> GetPublishedFormsAsync();
        Task<List<CreateEvaluationFormResponse>> GetAllFormsAsync();
        Task<bool> DeleteAsync(int id);
        Task<List<CreateEvaluationFormResponse>> GetMyFormsAsync();
        Task<DashboardStatisticsResponse> GetDashboardStatisticsAsync();

        // NEW - check if coordinator can still create forms
        Task<(bool canCreateSupervisor, bool canCreateExaminer)> GetFormCreationStatusAsync();
    }
}