using RSR.DAL.DTOs.Request.TaskReq;
using RSR.DAL.DTOs.Response;
using RSR.DAL.DTOs.Response.TaskRes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.Service.Task
{
    public  interface ITaskService
    {
        Task<BaseResponse> CreateTask(string SupervisorId, Guid GroupId, TaskRequest Request);
        Task<BaseResponse> UpdateTask(string SupervisorId, Guid GroupId, TaskRequest Request, Guid TaskId);
        Task<List<TaskResponse>> GetTasksByGroup(Guid GroupId, string supervisorId);
    }
}
