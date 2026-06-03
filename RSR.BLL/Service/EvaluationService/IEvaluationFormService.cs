using RSR.DAL.DTOs.Request.EvaluationRequest;
using RSR.DAL.DTOs.Response.EvaluationResponse;

namespace RSR.BLL.Services.EvaluationService
{
    public interface IEvaluationFormService
    {
        // CREATE FORM
        Task<CreateEvaluationFormResponse> CreateAsync(CreateEvaluationFormRequest request);

        // GET FORM WITH FIELDS
        Task<CreateEvaluationFormResponse> GetByIdAsync(int id);

        //publish
        Task<bool> PublishAsync(int id);
        //draft
         Task<bool> SetDraftAsync(int id);
        //Archive
        Task<bool> ArchiveAsync(int id);

        //update form
        Task<UpdateEvaluationFormResponse?> UpdateAsync(
                  int id,
            UpdateEvaluationFormRequest request);

        //get all published forms 
        Task<List<CreateEvaluationFormResponse>> GetPublishedFormsAsync();

        //Delete Draft Forms
        Task<bool> DeleteAsync(int id);

        //Get Form By Role
        Task<List<CreateEvaluationFormResponse>> GetMyFormsAsync();

        // Dashboard statistics
        Task<DashboardStatisticsResponse>  GetDashboardStatisticsAsync();

        Task<List<CreateEvaluationFormResponse>>
            GetAllFormsAsync();
    }
}