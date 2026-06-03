using Microsoft.EntityFrameworkCore;
using RSR.DAL.Data;
using RSR.DAL.DTOs.Response.Dashboared;
using RSR.DAL.Models.ProjectModel;
using RSR.DAL.Models.ScheduleModel;
using RSR.DAL.Models.TaskModel;
using RSR.DAL.Models.ThesisModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.Dashboared
{
    public  class DashboardRepo : IDashboardRepo
    {
        private readonly ApplicationDbContext _context;

        public DashboardRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        // coordinator
        public async Task<int> TotalProjects()
        {
            return  await _context.Projects.CountAsync();
        }
        public async Task<int> ActiveProjects()
        {
            return await _context.Projects.Where(p=>p.ProjectStatus == Status.InProgress).CountAsync();
        }
        public async Task<int> Examinations()
        {
            return await _context.Schedules.CountAsync();
        }

        // supervisor
        // static
        public async Task<int> MyGroups(string supervisorId)
        {
            var groups = await _context.Groups.Where(g=>g.SupervisorId == supervisorId).CountAsync();
            return groups;
        }
        public async Task<int> ThesisPending(string supervisorId)
        {
            var thesisPending = await _context.ThesisVersions.CountAsync(v => v.Thesis.Group.SupervisorId == supervisorId && !v.thesisFeedbacks.Any());
            return thesisPending;
        }
        public async Task<int> TaskSubmissionPending(string supervisorId)
        {
            var taskSubmiisionPending = await _context.TaskSubmissions.CountAsync( ts => ts.Task.SupervisorId == supervisorId && !ts.TaskSubmissionComments.Any(c=>c.Role == "Supervisor"));
            return taskSubmiisionPending;
        }

        public async Task<List<TaskSubmission>> TaskSubmissionNeedReview(string supervisorId)
        {
            var Submissions = await _context.TaskSubmissions
                             .Include(ts=>ts.Task).ThenInclude(t=>t.Group)
                             .Include(ts=>ts.Student).ThenInclude(s=>s.User)
                             .Where(ts => ts.Task.SupervisorId == supervisorId && ts.IsLatest
                             && !ts.TaskSubmissionComments.Any(u => u.UserId == supervisorId) 
                             ).ToListAsync();
            return Submissions;
        }
        public async Task<List<ThesisVersions>> ThesisVersionsNeedFeedback(string supervisorId)
        {
            var versions = await _context.ThesisVersions
                              .Include(v=>v.Thesis).ThenInclude(th=>th.Group).ThenInclude(g=>g.Project)
                             .Where(ts => ts.Thesis.Group.SupervisorId == supervisorId && ts.IsLatest
                             && !ts.thesisFeedbacks.Any(u => u.ReviwerId == supervisorId)
                             ).ToListAsync();
            return versions;
        }
        // student 
        public async Task <int>  TotalTask(string studentId)
        {
            return await _context.Tasks.CountAsync(t=>t.Group.Students.Any(s=>s.UserId == studentId));
        }
        public async Task<int> CompletedTask(string studentId)
        {
            return await _context.TaskSubmissions
                .Where(ts =>
                    ts.StudentId == studentId &&
                    ts.Status == SubmissionStatus.Approved)
                .Select(ts => ts.TaskId)
                .Distinct()
                .CountAsync();
        }
        public async Task<int> upComingDeadLine(string studentId)
        {
            return await _context.Tasks.CountAsync(t => t.Group.Students.Any(s => s.UserId == studentId) && t.DeadLine > DateTime.UtcNow);
        }
        public async Task <Status> ProjectStatus(string studentId)
        {
            return _context.Projects.Where(p=>p.Group.Students.Any(s=>s.UserId==studentId)).Select(p=>p.ProjectStatus).FirstOrDefault();
        }

        public async Task <List<UpComingDeadlineResponse>>UpComingThesis(string studentId)
        {
            return await _context.Thesis.Where(th=>th.Group.Students.Any(s=>s.UserId == studentId) && th.DeadLine >DateTime.UtcNow)
                .Where(th => !th.ThesisVersions.Any() || th.ThesisVersions.OrderByDescending(v => v.VersionNumber)
                .First().thesisFeedbacks.Any(f=>f.Decision !=  Decision.Approved)).
                 Select(th=>new UpComingDeadlineResponse
                {
                    Id = th.ThesisId , 
                    Type = "Thesis",
                    Title = th.Group.Project.ProjectName,
                    Deadline = th.DeadLine
                }).ToListAsync();
        }
        public async Task<List<UpComingDeadlineResponse>> UpComingTask(string studentId)
        {
            return await _context.Tasks.Where(t => t.Group.Students.Any(s => s.UserId == studentId) && t.DeadLine > DateTime.UtcNow )
                .Where(t=> !t.TaskSubmissions.Any() || t.TaskSubmissions.OrderByDescending(ts => ts.VersionNumber).First().Status != SubmissionStatus.Approved).
                Select(t => new UpComingDeadlineResponse
                {
                    Id = t.TaskId,
                    Type = "Task",
                    Title = t.Title,
                    Deadline = t.DeadLine
                }).ToListAsync();
        }



        // examiner 
        public async Task <int> TotalProjectsExaminer(string examinerId)
        {
            return await _context.DefenseExaminers.Where(e => e.ExaminerId == examinerId)
                                                  .Select(e => e.Schedule.GroupId)
                                                  .Distinct().CountAsync();
        }
        public async Task<int> UpComingExaminations(string examinerId)
        {
            return await _context.DefenseExaminers.Where(e => e.ExaminerId == examinerId && e.Schedule.Date > DateTime.UtcNow).CountAsync();
        }
        public async Task<List<Schedule>> UpComingExaminationsList (string examinerId)
        {
            var schedules = await _context.DefenseExaminers.Where(e=>e.ExaminerId == examinerId && e.Schedule.Date > DateTime.UtcNow)
               .Include(de=>de.Schedule).ThenInclude(s=>s.Group).ThenInclude(g=>g.Project)
                .Select(de => de.Schedule).OrderBy(d=>d.Date).ToListAsync();
            return schedules;
        }

    }
}
