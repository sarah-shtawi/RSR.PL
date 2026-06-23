using Microsoft.EntityFrameworkCore;
using RSR.DAL.Data;
using RSR.DAL.Models.Evaluation;

namespace RSR.DAL.Repository.EvaluationRepository
{
    public class EvaluationSubmissionRepository
        : IEvaluationSubmissionRepository
    {
        private readonly ApplicationDbContext _context;

        public EvaluationSubmissionRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EvaluationSubmission>CreateAsync(EvaluationSubmission submission)
        {
            await _context.EvaluationSubmissions.AddAsync(submission);
            await _context.SaveChangesAsync();
            return submission;
        }

        public async Task<bool>
         HasUserSubmittedAsync(
             int formId,
             Guid groupId,
             string userId)
        {
            return await _context.EvaluationSubmissions
                .AnyAsync(s =>
                    s.EvaluationFormId == formId &&
                    s.GroupId == groupId &&
                    s.SubmittedByUserId == userId);
        }
        // GET GROUP SUBMISSIONS

        public async Task<EvaluationSubmission?> GetUserSubmissionAsync(int formId, string userId)
        {
            return await _context.EvaluationSubmissions
                .Include(s => s.Answers)
                .FirstOrDefaultAsync(s =>
                    s.EvaluationFormId == formId &&
                    s.SubmittedByUserId == userId);
        }


        public async Task<List<EvaluationSubmission>>GetGroupSubmissionsAsync(Guid groupId)
        {
            return await _context.EvaluationSubmissions
                .Include(s => s.EvaluationForm)
                .Where(s => s.GroupId == groupId)
                .ToListAsync();
        }

         // COUNT SUBMISSIONS
         public async Task<int> CountAsync()
        {
            return await _context.EvaluationSubmissions.CountAsync();
        }


        public async Task<List<EvaluationSubmission>> GetUserSubmissionsByFormAsync(
    int formId, string userId)
        {
            return await _context.EvaluationSubmissions
                .Where(s =>
                    s.EvaluationFormId == formId &&
                    s.SubmittedByUserId == userId)
                .ToListAsync();
        }
    }
}