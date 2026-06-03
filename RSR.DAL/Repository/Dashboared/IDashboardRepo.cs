using RSR.DAL.Models.ProjectModel;
using RSR.DAL.Models.TaskModel;
using RSR.DAL.Models.ThesisModel;
using RSR.DAL.Models.ScheduleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSR.DAL.DTOs.Response.Dashboared;

namespace RSR.DAL.Repository.Dashboared
{
    public  interface IDashboardRepo
    {
        // coordinator
        Task<int> TotalProjects();
        Task<int> ActiveProjects();
        Task<int> Examinations();

        // supervisor 
        Task<int> MyGroups(string supervisorId);
        Task<int> ThesisPending(string supervisorId);
        Task<int> TaskSubmissionPending(string supervisorId);
        Task<List<TaskSubmission>> TaskSubmissionNeedReview(string supervisorId);
        Task<List<ThesisVersions>> ThesisVersionsNeedFeedback(string supervisorId);

        // student 
        Task<int> TotalTask(string studentId);
        Task<int> CompletedTask(string studentId);
        Task<int> upComingDeadLine(string studentId);
        Task<Status> ProjectStatus(string studentId);

        Task<List<UpComingDeadlineResponse>> UpComingThesis(string studentId);
        Task<List<UpComingDeadlineResponse>> UpComingTask(string studentId);

        // examiner 
        Task<int> TotalProjectsExaminer(string examinerId);
        Task<int> UpComingExaminations(string examinerId);
        Task<List<Schedule>> UpComingExaminationsList(string examinerId);

    }
}
