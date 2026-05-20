using RSR.DAL.Models.Evaluation;

namespace RSR.DAL.Repository.EvaluationRepository
{
    public interface IEvaluationFormRepository
    {
        Task<EvaluationForm> CreateAsync(EvaluationForm form);

        Task<EvaluationForm?> GetByIdAsync(int id);

        Task<EvaluationForm?> GetByIdWithFieldsAsync(int id);

        Task<EvaluationForm> UpdateAsync(EvaluationForm form);

        // GET ALL PUBLISHED FORMS
        Task<List<EvaluationForm>> GetPublishedFormsAsync();

        // DELETE DRAFT FORM
        Task<bool> DeleteAsync(int id);

        // GET FORMS BY ROLE
        Task<List<EvaluationForm>>
            GetPublishedFormsByRoleAsync(string role);
    }
}
    