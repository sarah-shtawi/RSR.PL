using RSR.DAL.Data;
using RSR.DAL.Models.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RSR.DAL.Repository.EvaluationRepository
{
    public class EvaluationSubmissionRepository : IEvaluationSubmissionRepository
    {
        private readonly ApplicationDbContext _context;

        public EvaluationSubmissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EvaluationSubmission> CreateAsync(EvaluationSubmission submission)
        {
            await _context.EvaluationSubmissions.AddAsync(submission);

            await _context.SaveChangesAsync();

            return submission;
        }
        //NO Duplicate submission

        public async Task<bool> HasUserSubmittedAsync(
               int formId,
             string userId)
        {
            return await _context.EvaluationSubmissions
                .AnyAsync(s =>
                    s.EvaluationFormId == formId &&
                    s.SubmittedByUserId == userId);
        }
    }
}