using RSR.DAL.Models.TaskModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.TaskSubmissionRepo
{
    public interface ITaskSubmissionRepository
    {
        System.Threading.Tasks.Task AddSubmission(TaskSubmission taskSubmission);
        System.Threading.Tasks.Task<TaskSubmission?> GetLastSubmission(Guid TaskId);
        Task<TaskSubmission> UpdateTaskSubmission(TaskSubmission taskSubmission);
        Task<TaskSubmission?> GetSubmissionById(Guid TaskSubmissionId);
        Task<TaskSubmission> DeleteSubmission(TaskSubmission taskSubmission);
    }
}
