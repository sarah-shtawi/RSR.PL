using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.TaskSubmission;
using RSR.DAL.DTOs.Request.TaskReq;
using System.Security.Claims;

namespace RSR.PL.Areas.Student
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles =("Student"))]
    public class TaskSubmissionController : ControllerBase
    {
        private readonly ITaskSubmissionService _taskSubmissionService;

        public TaskSubmissionController(ITaskSubmissionService taskSubmissionService)
        {
            _taskSubmissionService = taskSubmissionService;
        }

        [HttpPost("tasks/{TaskId}/submissions")]
        public async Task <IActionResult> AddSubmission([FromRoute] Guid TaskId , [FromRoute] Guid GroupId, [FromForm] TaskSubmissionRequest Request  )
        {
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _taskSubmissionService.AddTaskSubmission(Request , studentId, TaskId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPatch("submission/{TaskSubmissionId}")]
        public async Task <IActionResult> UpdateTaskSubmission( TaskSubmissionRequest Request,[FromRoute] Guid TaskSubmissionId)
        {
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _taskSubmissionService.UpdateTaskSubmission(Request , studentId, TaskSubmissionId);
            if (!result.Success) 
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpDelete("Delete/SubmissionId/{TaskSubmissionId}")]
        public async Task <IActionResult> DeleteSubmission([FromRoute] Guid TaskSubmissionId)
        {
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _taskSubmissionService.DeleteSubmission(TaskSubmissionId , studentId);
            if (!result.Success) 
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


    }
}
