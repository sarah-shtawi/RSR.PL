using RSR.DAL.DTOs.Request.TaskReq;
using RSR.DAL.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.Service.TaskSubmission
{
    public  interface ITaskSubmissionService
    {
        Task<BaseResponse> AddTaskSubmission(TaskSubmissionRequest TaskSubmission, string StudentId, Guid TaskId);
        Task<BaseResponse> UpdateTaskSubmission(TaskSubmissionRequest Request, string StudentId, Guid SubmissionTaskId);
        Task<BaseResponse> ReviewForSubmission(Guid submissionId, string supervisorId, ReviewTaskSubmission request);
        Task<BaseResponse> DeleteSubmission(Guid submissionId, string studentId);
    }
}
