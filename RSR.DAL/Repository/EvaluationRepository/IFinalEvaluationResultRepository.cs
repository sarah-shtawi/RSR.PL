using RSR.DAL.Models.Evaluation;

namespace RSR.DAL.Repository.EvaluationRepository
{
    public interface IFinalEvaluationResultRepository
    {
        // =========================
        // CREATE
        // =========================
        Task<FinalEvaluationResult>
            CreateAsync(FinalEvaluationResult result);

        // =========================
        // GET BY GROUP ID
        // =========================
        Task<FinalEvaluationResult?>
            GetByGroupIdAsync(Guid groupId);

        // =========================
        // GET PUBLISHED RESULT
        // =========================
        Task<FinalEvaluationResult?>
            GetPublishedByGroupIdAsync(Guid groupId);

        // =========================
        // GET SUPERVISOR RESULTS
        // =========================
        Task<List<FinalEvaluationResult>>
            GetSupervisorResultsAsync(string supervisorId);

        // =========================
        // GET BY ID
        // =========================
        Task<FinalEvaluationResult?>
            GetByIdAsync(int id);

        // =========================
        // SAVE CHANGES
        // =========================
        Task<bool> SaveChangesAsync();

        //final Groups Grades
        Task<List<FinalEvaluationResult>> GetAllFinalResultsAsync();
    }
}