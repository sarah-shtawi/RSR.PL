using RSR.DAL.Models.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.EvaluationRepository
{
    public interface IEvaluationSubmissionRepository
    {
        Task<EvaluationSubmission> CreateAsync(EvaluationSubmission submission);

        Task<bool> HasUserSubmittedAsync(
             int formId,
             string userId);
    }
}