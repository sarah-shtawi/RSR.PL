using RSR.DAL.Models.Evaluation;

namespace RSR.DAL.Repository.EvaluationRepository
{
    public interface IEvaluationFormRepository
    {
        Task<EvaluationForm> CreateAsync(EvaluationForm form);
        Task<EvaluationForm?> GetByIdAsync(int id);
        Task<EvaluationForm?> GetByIdWithFieldsAsync(int id);
        Task<EvaluationForm> UpdateAsync(EvaluationForm form);
        Task<List<EvaluationForm>> GetPublishedFormsAsync();
        Task<List<EvaluationForm>> GetAllFormsAsync();
        Task<bool> DeleteAsync(int id);
        Task<List<EvaluationForm>> GetPublishedFormsByRoleAsync(string role);
        Task<int> CountAsync();
        Task<int> CountPublishedAsync();

        // NEW - filter by semester
        Task<List<EvaluationForm>> GetFormsBySemesterAsync(Guid semesterId);
        Task<List<EvaluationForm>> GetPublishedFormsBySemesterAsync(Guid semesterId);
        Task<List<EvaluationForm>> GetPublishedFormsByRoleAndSemesterAsync(string role, Guid semesterId);
    }
}