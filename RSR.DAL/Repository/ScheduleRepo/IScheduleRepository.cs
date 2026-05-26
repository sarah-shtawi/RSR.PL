using RSR.DAL.DTOs.Response.Schedule;
using RSR.DAL.Models.ScheduleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.ScheduleRepo
{
    public  interface IScheduleRepository
    {
        Task<Schedule?> CreateSchedule(Schedule schedule);
        Task<Schedule> UpdateSchedule(Schedule schedule);
        Task<Schedule?> GetScheduleById(Guid ScheduleId);
        Task RemoveExaminers(Guid scheduleId);
        Task<List<ScheduleResponse>> getSchedulesForCoordinator();

        Task<List<ScheduleResponse>> getSchedulesForSupervisor(string supervisorId);
    }
}
