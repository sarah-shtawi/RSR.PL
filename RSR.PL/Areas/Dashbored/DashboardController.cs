using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.Dashbored;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace RSR.PL.Areas.Dashbored
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboredService _dashboredService;

        public DashboardController(IDashboredService dashboredService)
        {
            _dashboredService = dashboredService;
        }

        [Authorize(Roles =("Coordinator"))]
        [HttpGet("dashboard-coordinator")]
        public async Task <IActionResult> GetStatistics()
        {
            var Statistics = await _dashboredService.CoordinatorDashboared();
            return Ok( new { message = "success", Statistics });
        }



        [Authorize(Roles =("Supervisor"))]
        [HttpGet("dashboard-supervisor")]
        public async Task<IActionResult> GetStatisticsSupervisor ()
        {
            var SupervisorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Statistics = await _dashboredService.SupervisorDashboard(SupervisorId);
            return Ok(new { message = "success", Statistics });
        }

        [Authorize(Roles = ("Supervisor"))]
        [HttpGet("dashboard-tasks")]
        public async Task<IActionResult> GetTaskNeedReview()
        {
            var supervisorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Tasks = await _dashboredService.TaskSubmissionNeedReview(supervisorId);
            return Ok(new { message = "success", Tasks });
        }
        [Authorize(Roles = ("Supervisor"))]
        [HttpGet("dashboard-thesis")]
        public async Task<IActionResult> GetThesisNeedFeedback()
        {
            var supervisorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Thesis = await _dashboredService.ThesisVersionsNeedFeedback(supervisorId);
            return Ok(new { message = "success", Thesis });
        }



        [Authorize(Roles = ("Student"))]
        [HttpGet("dashboard-student")]
        public async Task<IActionResult> GetStatisticsStudent()
        {
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Statistics = await _dashboredService.StudentDashboard(studentId);
            return Ok(new { message = "success", Statistics });
        }

        [Authorize(Roles = ("Student"))]
        [HttpGet("dashboard-deadlines")]
        public async Task<IActionResult> upComingDeadlines()
        {
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var deadlines = await _dashboredService.upComingDeadlines(studentId);
            return Ok(new { message = "success", deadlines });
        }


        [Authorize(Roles = ("Examiner"))]
        [HttpGet("dashboard-examiner")]
        public async Task<IActionResult> GetStatisticsExaminer()
        {
            var examinerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Statistics = await _dashboredService.ExaminerDashboard(examinerId);
            return Ok(new { message = "success", Statistics });
        }


        [Authorize(Roles = ("Examiner"))]
        [HttpGet("dashboard-UpComingExamination")]
        public async Task<IActionResult> UpComingExamination()
        {
            var examinerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var examinations  = await _dashboredService.ExaminationForExaminer(examinerId);
            return Ok(new { message = "success", examinations });
        }

       

    }
}
