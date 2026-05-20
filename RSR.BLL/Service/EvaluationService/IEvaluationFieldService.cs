using RSR.DAL.DTOs.Request.EvaluationRequest;
using RSR.DAL.DTOs.Response.EvaluationResponse;

namespace RSR.BLL.Services.EvaluationService
{
    public interface IEvaluationFieldService
    {
         // CREATE FIELD
         Task<CreateEvaluationFieldResponse> CreateAsync(
            CreateEvaluationFieldRequest request,
            int evaluationFormId);

         // DELETE FIELD
         Task<bool> DeleteAsync(int id);

         // UPDATE FIELD
         Task<UpdateEvaluationFieldResponse?> UpdateAsync(
            int id,
            UpdateEvaluationFieldRequest request);
    }
}