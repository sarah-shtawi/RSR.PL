using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RSR.DAL.Data;
using RSR.DAL.DTOs.Response.Schedule;
using RSR.DAL.Models.ScheduleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.ScheduleRepo
{
    public  class ScheduleRepository : IScheduleRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public ScheduleRepository(ApplicationDbContext context , IConfiguration configuration )
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task <Schedule?> CreateSchedule(Schedule schedule)
        {
            var scheduleDB = await _context.Schedules.AddAsync(schedule);
            await _context.SaveChangesAsync();
            return schedule;
        }
        public async Task <Schedule> UpdateSchedule(Schedule schedule)
        {
            _context.Schedules.Update(schedule);
            await _context.SaveChangesAsync();
            return schedule;
        }
        public async Task <Schedule?> GetScheduleById(Guid ScheduleId)
        {
            var schedule = await _context.Schedules.Include(s=>s.DefenseExaminers).FirstOrDefaultAsync(s=>s.ScheduleId == ScheduleId);
            return schedule;
        }
        public async Task RemoveExaminers(Guid scheduleId)
        {
            var examiners = await _context.DefenseExaminers.Where(d => d.ScheduleId == scheduleId).ToListAsync();
            _context.DefenseExaminers.RemoveRange(examiners);
            await _context.SaveChangesAsync();

        }
        public async Task <List<ScheduleResponse>> getSchedulesForCoordinator()
        {
            var schedules = await _context.Schedules
                .Select(s => new ScheduleResponse
                {
                    ScheduleId = s.ScheduleId,
                    GroupName = s.Group.GroupName,
                    SupervisorName = s.Group.Supervisor.User.FullName,
                    ProjectName = s.Group.Project.ProjectName,
                    Location = s.Location,
                    Date = s.Date,
                    Notes = s.Notes,
                    ThesisURL = string.IsNullOrEmpty(s.Group.Thesis.ThesisFile) ? null : $"{_configuration["URL:BaseUrl"]}/files/Thesis/{s.Group.Thesis.ThesisFile}",
                    Students = s.Group.Students.Select(st => st.User.FullName).ToList(),
                    Examiners = s.DefenseExaminers.Select(e=>e.Examiner.User.FullName).ToList()
                }).ToListAsync();
            return schedules;
        }
        public async Task <List<ScheduleResponse>> getSchedulesForSupervisor(string supervisorId)
        {
            var schedules = await _context.Schedules.Where(s=>s.Group.SupervisorId == supervisorId)
                .Select(s=> new ScheduleResponse
                {
                    ScheduleId = s.ScheduleId,
                    GroupName = s.Group.GroupName,
                    SupervisorName = s.Group.Supervisor.User.FullName,
                    ProjectName = s.Group.Project.ProjectName,
                    Location = s.Location,
                    Date = s.Date,
                    Notes = s.Notes,
                    ThesisURL = string.IsNullOrEmpty(s.Group.Thesis.ThesisFile) ? null : $"{_configuration["URL:BaseUrl"]}/files/Thesis/{s.Group.Thesis.ThesisFile}",
                    Students = s.Group.Students.Select(st => st.User.FullName).ToList(),
                    Examiners = s.DefenseExaminers.Select(e => e.Examiner.User.FullName).ToList(),
                }).ToListAsync();
            return schedules;
        }
        public async Task <ScheduleResponse?> GetScheduleForStudent(string studentId)
        {
            var schedule = await _context.Schedules.Where(s=>s.Group.Students.Any(s=>s.UserId == studentId)).Select(s=> new ScheduleResponse
            {
                ScheduleId = s.ScheduleId,
                GroupName = s.Group.GroupName,
                SupervisorName = s.Group.Supervisor.User.FullName,
                ProjectName = s.Group.Project.ProjectName,
                Location = s.Location,
                Date = s.Date,
                Notes = s.Notes,
                ThesisURL = string.IsNullOrEmpty(s.Group.Thesis.ThesisFile) ? null : $"{_configuration["URL:BaseUrl"]}/files/Thesis/{s.Group.Thesis.ThesisFile}",
                Students = s.Group.Students.Select(st => st.User.FullName).ToList(),
                Examiners = s.DefenseExaminers.Select(e => e.Examiner.User.FullName).ToList(),
            }).FirstOrDefaultAsync();

            return schedule;
        }
        public async Task <List<ScheduleResponse?>> GetSchedulesForExaminer(string ExaminerId)
        {
            var schedules = await _context.Schedules.Where(s => s.DefenseExaminers.Any(e => e.ExaminerId == ExaminerId)).Select(s => new ScheduleResponse
            {
                ScheduleId = s.ScheduleId,
                GroupName = s.Group.GroupName,
                SupervisorName = s.Group.Supervisor.User.FullName,
                ProjectName = s.Group.Project.ProjectName,
                Location = s.Location,
                Date = s.Date,
                Notes = s.Notes,
                ThesisURL = string.IsNullOrEmpty(s.Group.Thesis.ThesisFile) ? null : $"{_configuration["URL:BaseUrl"]}/files/Thesis/{s.Group.Thesis.ThesisFile}",
                Students = s.Group.Students.Select(st => st.User.FullName).ToList(),
                Examiners = s.DefenseExaminers.Select(e => e.Examiner.User.FullName).ToList()
            }).ToListAsync();
            return schedules;
        }


        public async Task DeleteSchedule(Schedule schedule)
        {
            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
        }
    }
}
