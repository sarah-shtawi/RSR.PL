using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RSR.DAL.DTOs.Response.Dashboared;
using RSR.DAL.DTOs.Response.ThesisRes;
using RSR.DAL.Models.User;
using RSR.DAL.Repository.Dashboared;
using RSR.DAL.Models.ScheduleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.Service.Dashbored
{
    public class DashboredService : IDashboredService
    {
        private readonly IDashboardRepo _coordinatorRepoDash;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboredService(IDashboardRepo coordinatorRepoDash , UserManager<ApplicationUser> userManager)
        {
            _coordinatorRepoDash = coordinatorRepoDash;
            _userManager = userManager;
        }

        public async Task <CoordinatorDashboared> CoordinatorDashboared()
        {
            var totalProject = await _coordinatorRepoDash.TotalProjects();
            var totalUser = await _userManager.Users.CountAsync();
            var ActiveProject = await _coordinatorRepoDash.ActiveProjects();
            var Examinations = await _coordinatorRepoDash.Examinations();

            var coordinatorDashbored = new CoordinatorDashboared
            {
                TotalProjects = totalProject,
                TotalUsers = totalUser,
                ActiveProjects = ActiveProject,
                Examinations = Examinations
            };
            return coordinatorDashbored;
        }
     
        // supervisor 
        public async Task <SupervisorDashboard> SupervisorDashboard(string supervisorId)
        {
            var groups = await _coordinatorRepoDash.MyGroups(supervisorId);
            var taskPending = await _coordinatorRepoDash.TaskSubmissionPending(supervisorId);
            var thesisPending = await _coordinatorRepoDash.ThesisPending(supervisorId);

            var dashboardSupervisor = new SupervisorDashboard
            {
                MyGroups = groups,
                ThesisPending = thesisPending,
                TaskSubmissionsPending = taskPending,
                TotalPendingReviews = taskPending + thesisPending
            };
            return dashboardSupervisor;
        }

        public async Task <List<TaskDashboardResponse>>TaskSubmissionNeedReview(string supervisorId)
        {
            var submissions = await _coordinatorRepoDash.TaskSubmissionNeedReview(supervisorId);
            var response = submissions
                 .Select(ts => new TaskDashboardResponse
                   {
                     TaskId = ts.TaskId,
                    TaskSubmissionId = ts.TaskSubmissionId,
                    title = ts.Task.Title,
                    GroupName = ts.Task.Group.GroupName,
                    StudentName = ts.Student.User.FullName,
                    SubmittedAt = ts.SubmittedAt
                   }).ToList();
            return response;
        }

        public async Task<List<ThesisDashboardResponse>> ThesisVersionsNeedFeedback(string supervisorId)
        {
            var versions = await _coordinatorRepoDash.ThesisVersionsNeedFeedback(supervisorId);
            var response = versions.Select(v => new ThesisDashboardResponse
            {
                ThesisId = v.ThesisId,
                ThesisVersionId = v.VersionId,
                GroupId = v.Thesis.GroupId,
                ProjectName = v.Thesis.Group.Project.ProjectName,
                GroupName = v.Thesis.Group.GroupName,
                UploadedAt = v.UploadedAt,
            }).ToList();
            return response;
        }

        
        // student 
        public async Task<StudentDashboard> StudentDashboard(string studentId)
        {
            var totalTask = await _coordinatorRepoDash.TotalTask(studentId);
            var CompletedTask = await _coordinatorRepoDash.CompletedTask(studentId);
            var UpComingDeadLine = await _coordinatorRepoDash.upComingDeadLine(studentId);
            var projectStatus = await _coordinatorRepoDash.ProjectStatus(studentId);

            var studentDash = new StudentDashboard
            {
                TotalTask = totalTask,
                CompletedTask = CompletedTask,  
                ProjectStatus = projectStatus
            };
            return studentDash;
        }

        public async Task<List<UpComingDeadlineResponse>>upComingDeadlines(string studentId)
        {
            var tasks = await _coordinatorRepoDash.UpComingTask(studentId);
            var thesis = await _coordinatorRepoDash.UpComingThesis(studentId);

            return tasks.Concat(thesis).OrderBy(d => d.Deadline).ToList();
        }


        // examiner 
        public async Task<ExaminerDashboard> ExaminerDashboard(string ExaminerId)
        {
            var totalProjects = await _coordinatorRepoDash.TotalProjectsExaminer(ExaminerId);

            var UpComingExaminations = await _coordinatorRepoDash.UpComingExaminations(ExaminerId);

            var examinerDashboard = new ExaminerDashboard
            {
                TotalProjects = totalProjects,
                UpComingExaminations = UpComingExaminations
            };
            return examinerDashboard;
        }
        public async Task <List<ExaminerExaminationResponse>> ExaminationForExaminer(string ExaminerId)
        {
            var examinations = await _coordinatorRepoDash.UpComingExaminationsList(ExaminerId);

            var response = examinations.Select(e => new ExaminerExaminationResponse
            {
                ScheduleId = e.ScheduleId,
                Location = e.Location,
                Date = e.Date,
                GroupName = e.Group.GroupName,
                ProjectName = e.Group.Project.ProjectName,
            }).ToList();

            return response;
        }
       

    }
}
