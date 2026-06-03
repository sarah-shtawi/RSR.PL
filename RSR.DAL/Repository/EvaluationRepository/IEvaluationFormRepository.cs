using RSR.DAL.Models.Evaluation;

namespace RSR.DAL.Repository.EvaluationRepository
{
    public interface IEvaluationFormRepository
    {
        // =========================
        // CREATE
        // =========================
        Task<EvaluationForm>
            CreateAsync(EvaluationForm form);

        // =========================
        // GET BY ID
        // =========================
        Task<EvaluationForm?>
            GetByIdAsync(int id);

        // =========================
        // GET WITH FIELDS
        // =========================
        Task<EvaluationForm?>
            GetByIdWithFieldsAsync(int id);

        // =========================
        // UPDATE
        // =========================
        Task<EvaluationForm>
            UpdateAsync(EvaluationForm form);

        // =========================
        // GET ALL PUBLISHED
        // =========================
        Task<List<EvaluationForm>>
            GetPublishedFormsAsync();

        // =========================
        // DELETE
        // =========================
        Task<bool>
            DeleteAsync(int id);

        // =========================
        // GET FORMS BY ROLE
        // =========================
        Task<List<EvaluationForm>> GetPublishedFormsByRoleAsync(string role);

        // =========================
        // COUNT ALL FORMS
        // =========================
        Task<int>CountAsync();

        // =========================
        // COUNT PUBLISHED FORMS
        // =========================
        Task<int> CountPublishedAsync();

        Task<List<EvaluationForm>>GetAllFormsAsync();
    }
}