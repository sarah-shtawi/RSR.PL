using RSR.DAL.DTOs.Request.ScheduleReq;
using RSR.DAL.DTOs.Response;
using RSR.DAL.DTOs.Response.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.Service.Schedule
{
    public  interface IScheduleService
    {
        System.Threading.Tasks.Task<BaseResponse> CreateSchedule(ScheduleRequest request, string coordinatorId);
        System.Threading.Tasks.Task<BaseResponse> UpdateSchedule(ScheduleRequest request, string coordinatorId, Guid scheduleId);
        Task<List<ScheduleResponse>> GetSchedulesForCoordinator();
        Task<List<ScheduleResponse>> GetSchedulesForSupervisor(string supervisorId);
    }
}
