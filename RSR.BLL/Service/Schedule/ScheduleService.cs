using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RSR.DAL.Data;
using RSR.DAL.DTOs.Request.ScheduleReq;
using RSR.DAL.DTOs.Response;
using RSR.DAL.DTOs.Response.Schedule;
using RSR.DAL.Migrations;
using RSR.DAL.Models.ScheduleModel;
using RSR.DAL.Models.User;
using RSR.DAL.Repository.GroupRepo;
using RSR.DAL.Repository.ScheduleRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.Service.Schedule
{
    public  class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ScheduleService(IScheduleRepository scheduleRepository , IGroupRepository groupRepository, UserManager <ApplicationUser> userManager , ApplicationDbContext context)
        {
            _scheduleRepository = scheduleRepository;
            _groupRepository = groupRepository;
            _userManager = userManager;
            _context = context;
        }

        public async System.Threading.Tasks.Task<BaseResponse> CreateSchedule(ScheduleRequest request , string coordinatorId)
        {
            var group = await _groupRepository.GroupByIdRepo(request.GroupId);
            if (group == null) 
            {
               return new BaseResponse{
                   Success = false,
                   Message = "group is not found"
               };
            }
            var hasFrozenThesis = await _context.ThesisVersions.AnyAsync(v => v.Thesis.GroupId == request.GroupId && v.IsFrozen);
            if (!hasFrozenThesis)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "This group does not have a frozen thesis yet"
                };
            }

            if (group.Schedule != null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "This group already has a schedule"
                };
            }
            if (request.Date <= DateTime.UtcNow)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Defense date must be in the future"
                };
            }


            var examiners = await _userManager.Users.Where(u => request.ExaminersIds.Contains(u.Id) && u.ExaminerProfile != null).CountAsync();
            if(examiners != request.ExaminersIds.Count)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "One or more examiners are invalid"
                };
            }

            var schedule = new RSR.DAL.Models.ScheduleModel.Schedule
            {
                GroupId = request.GroupId,
                CoordinatorId = coordinatorId,
                Date = request.Date,
                Location = request.Location,
                Notes = request.Notes,
            };
            schedule.DefenseExaminers = request.ExaminersIds.Select(id => new DefenseExaminer
            {
                ExaminerId = id,
            }).ToList();


            await _scheduleRepository.CreateSchedule(schedule);

            return new BaseResponse
            {
                Success = true,
                Message = "Schedule created successfully"
            };
        }

        public async System.Threading.Tasks.Task<BaseResponse> UpdateSchedule(ScheduleRequest request, string coordinatorId, Guid scheduleId)
        {
            var schedule = await _scheduleRepository.GetScheduleById(scheduleId);
            if (schedule == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Schedule not found"
                };
            }
            if (schedule.CoordinatorId != coordinatorId)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "You are not allowed to update this schedule"
                };
            }
            // validate examiners
            var examiners = await _userManager.Users.Where(u => request.ExaminersIds.Contains(u.Id) && u.ExaminerProfile != null).CountAsync();

            if (examiners != request.ExaminersIds.Count)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "One or more examiners are invalid"
                };
            }
            // remove old  examiners 
            await _scheduleRepository.RemoveExaminers(scheduleId);

            schedule.DefenseExaminers.Clear();


            schedule.Date = request.Date;
            schedule.Location = request.Location;
            schedule.Notes = request.Notes;

  
            // create new examiners
            var newExaminers = request.ExaminersIds
                .Select(id => new DefenseExaminer
                {
                    ExaminerId = id,
                    ScheduleId = schedule.ScheduleId
                }).ToList();

            // add directly to dbset
            await _context.DefenseExaminers
                .AddRangeAsync(newExaminers);


            await _scheduleRepository.UpdateSchedule(schedule);
            return new BaseResponse
            {
                Success = true,
                Message = "Schedule updated successfully"
            };
        }

        public async Task <List<ScheduleResponse>> GetSchedulesForCoordinator()
        {
            var allSchedules = await _scheduleRepository.getSchedulesForCoordinator();
            return allSchedules;
        }

        public async Task <List<ScheduleResponse>> GetSchedulesForSupervisor(string supervisorId)
        {         
            var Schedules = await _scheduleRepository.getSchedulesForSupervisor(supervisorId);
            return Schedules;
        }
        public async Task <ScheduleResponse> GetScheduleStudent(string studentId)
        {
            var schedule = await _scheduleRepository.GetScheduleForStudent(studentId);
            return schedule;
        }

        public async Task <List<ScheduleResponse>> GetSchedulesExaminer(string examinerId)
        {
            var Schedules = await _scheduleRepository.GetSchedulesForExaminer(examinerId);
            return Schedules;
        }


        public async Task <BaseResponse> RemoveSchedule (Guid scheduleId)
        {
            var schedule = await _scheduleRepository.GetScheduleById(scheduleId);
            if(schedule == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Schedule not found"
                };
            }
            await _scheduleRepository.DeleteSchedule(schedule);
            return new BaseResponse
            {
                Success = true ,
                Message = "Schedule deleted Successfully"
            };
        }
    }
}
