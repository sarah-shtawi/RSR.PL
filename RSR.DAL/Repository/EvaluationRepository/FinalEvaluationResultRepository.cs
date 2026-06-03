using Microsoft.EntityFrameworkCore;
using RSR.DAL.Data;
using RSR.DAL.Models.Evaluation;

namespace RSR.DAL.Repository.EvaluationRepository
{
    public class FinalEvaluationResultRepository
        : IFinalEvaluationResultRepository
    {
        private readonly ApplicationDbContext _context;

        public FinalEvaluationResultRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // CREATE
        // =========================
        public async Task<FinalEvaluationResult>
            CreateAsync(FinalEvaluationResult result)
        {
            await _context.FinalEvaluationResults
                .AddAsync(result);

            await _context.SaveChangesAsync();

            return result;
        }

        // =========================
        // GET BY GROUP ID
        // =========================
        public async Task<FinalEvaluationResult?>
            GetByGroupIdAsync(Guid groupId)
        {
            return await _context.FinalEvaluationResults
                .FirstOrDefaultAsync(f =>
                    f.GroupId == groupId);
        }

        // =========================
        // GET PUBLISHED RESULT
        // =========================
        public async Task<FinalEvaluationResult?>
            GetPublishedByGroupIdAsync(Guid groupId)
        {
            return await _context.FinalEvaluationResults
                .FirstOrDefaultAsync(f =>
                    f.GroupId == groupId &&
                    f.Status == "Published");
        }

        // =========================
        // GET SUPERVISOR RESULTS
        // =========================
        public async Task<List<FinalEvaluationResult>>
            GetSupervisorResultsAsync(
                string supervisorId)
        {
            return await _context
                .FinalEvaluationResults
                .Include(f => f.Group)
                .Where(f =>
                    f.Group.SupervisorId
                        == supervisorId
                    &&
                    f.Status == "Published")
                .ToListAsync();
        }

        // =========================
        // GET BY ID
        // =========================
        public async Task<FinalEvaluationResult?>
            GetByIdAsync(Guid id)
        {
            return await _context.FinalEvaluationResults
                .FirstOrDefaultAsync(f => f.GroupId == id);
        }

        // =========================
        // SAVE CHANGES
        // =========================
        public async Task<bool> SaveChangesAsync()
        {
            var result =
                await _context.SaveChangesAsync();

            return result > 0;
        }
        //Final Groups Grades
        public async Task<List<FinalEvaluationResult>>
            GetAllFinalResultsAsync()
        {
            return await _context
                .FinalEvaluationResults
                .Include(f => f.Group)
                .ThenInclude(g => g.Supervisor)
                .ThenInclude(s => s.User)
                .ToListAsync();
        }
    }
}