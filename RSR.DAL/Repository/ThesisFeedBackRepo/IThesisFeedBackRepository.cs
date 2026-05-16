using RSR.DAL.Models.ThesisModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.ThesisFeedBackRepo
{
    public  interface IThesisFeedBackRepository
    {
        Task<ThesisFeedback?> GetLastFeedback(Guid ThesisVersionId);
        Task<bool> HasFeedBack(Guid ThesisVersionId);
        Task<ThesisFeedback> AddFeedback(ThesisFeedback feedback);
        Task<ThesisFeedback?> GetByVersionAndReviewer(Guid versionId, string reviewerId);
    }
}
