using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.Schedule;
using RSR.DAL.DTOs.Request.ScheduleReq;
using System.Security.Claims;

namespace RSR.PL.Areas.Coordinator
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [Authorize(Roles =("Coordinator"))]
        [HttpPost("create-schedule")]
        public async Task <IActionResult> CreateSchedule(ScheduleRequest request)
        {
            var coordinatorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _scheduleService.CreateSchedule(request , coordinatorId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok( new { message = "success", result });
        }



        [Authorize(Roles = ("Coordinator"))]
        [HttpPatch("update-schedule/{scheduleId}")]
        public async Task<IActionResult> UpdateSchedule([FromBody] ScheduleRequest request , [FromRoute] Guid scheduleId)
        {
            var coordinatorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _scheduleService.UpdateSchedule(request, coordinatorId, scheduleId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(new { message = "success", result });
        }



        [Authorize(Roles = ("Coordinator"))]
        [HttpGet("all-schedules")]
        public async Task<IActionResult> GetSchedules()
        {
            var result = await _scheduleService.GetSchedulesForCoordinator();
            return Ok(new { message = "success", result });
        }




        [Authorize(Roles = ("Supervisor"))]
        [HttpGet("schedules-supervisor")]
        public async Task<IActionResult> GetSchedulesForSupervisor()
        {
            var supervisorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _scheduleService.GetSchedulesForSupervisor(supervisorId);
            return Ok(new { message = "success", result });
        }



        [Authorize(Roles = ("Student"))]
        [HttpGet("schedule-student")]
        public async Task<IActionResult> GetSchedulesForStudent()
        {
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _scheduleService.GetScheduleStudent(studentId);
            return Ok(new { message = "success", result });
        }


        [Authorize(Roles = ("Examiner"))]
        [HttpGet("schedules-Examiner")]
        public async Task<IActionResult> GetSchedulesForExaminer()
        {
            var ExaminerId  = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _scheduleService.GetSchedulesExaminer(ExaminerId);
            return Ok(new { message = "success", result });
        }

        [Authorize(Roles = ("Coordinator"))]
        [HttpDelete("remove-schedule/scheduleId/{scheduleId}")]
        public async Task<IActionResult> RemoveSchedule([FromRoute] Guid scheduleId)
        {
            var result = await _scheduleService.RemoveSchedule(scheduleId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(new { message = "success", result });
        }

    }
}
