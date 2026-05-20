using Microsoft.EntityFrameworkCore;
using RSR.DAL.Data;
using RSR.DAL.Models.ThesisModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.ThesisFeedBackRepo
{
    public  class ThesisFeedBackRepository : IThesisFeedBackRepository
    {
        private readonly ApplicationDbContext _context;

        public ThesisFeedBackRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ThesisFeedback?> GetLastFeedback(Guid ThesisVersionId)
        {
            var LastFeedback = await _context.ThesisFeedbacks
                .Where(f => f.VersionId == ThesisVersionId)
                .OrderByDescending(f=>f.CreateAt)
                .FirstOrDefaultAsync();
            return LastFeedback;
        }
        public async Task<bool> HasFeedBack(Guid ThesisVersionId)
        {
            var hasFeedback = await _context.ThesisFeedbacks.AnyAsync(v=> v.VersionId == ThesisVersionId);
            return hasFeedback;
        }
        public async Task <ThesisFeedback> AddFeedback(ThesisFeedback feedback)
        {
            var feedBack = await _context.ThesisFeedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();
            return feedback;
        }
        public async Task<ThesisFeedback?> GetByVersionAndReviewer(Guid versionId, string reviewerId)
        {
            return await _context.ThesisFeedbacks
                .FirstOrDefaultAsync(f =>
                    f.VersionId == versionId &&
                    f.ReviwerId == reviewerId);
        }



    }
}
