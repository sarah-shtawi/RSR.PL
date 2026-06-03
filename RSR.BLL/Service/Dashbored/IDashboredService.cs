using RSR.DAL.DTOs.Response.Dashboared;
using RSR.DAL.DTOs.Response.ThesisRes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.Service.Dashbored
{
    public  interface IDashboredService
    {
        // coordinator
        Task<CoordinatorDashboared> CoordinatorDashboared();


        // supervisor 
        Task<SupervisorDashboard> SupervisorDashboard(string supervisorId);
        Task<List<TaskDashboardResponse>> TaskSubmissionNeedReview(string supervisorId);
        Task<List<ThesisDashboardResponse>> ThesisVersionsNeedFeedback(string supervisorId);

        // student
        Task<StudentDashboard> StudentDashboard(string studentId);
        Task<List<UpComingDeadlineResponse>> upComingDeadlines(string studentId);

        // examiner 
        Task<ExaminerDashboard> ExaminerDashboard(string ExaminerId);
        Task<List<ExaminerExaminationResponse>> ExaminationForExaminer(string ExaminerId);
            
    }
}
