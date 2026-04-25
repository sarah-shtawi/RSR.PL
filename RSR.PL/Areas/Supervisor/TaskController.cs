using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.Task;
using RSR.DAL.DTOs.Request.TaskReq;
using System.Security.Claims;

namespace RSR.PL.Areas.Supervisor
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Supervisor")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost("create/{GroupId}")]
        public async Task <IActionResult> CreateTask([FromRoute] Guid GroupId , [FromForm] TaskRequest Request)
        {
            var supervisorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _taskService.CreateTask(supervisorId, GroupId , Request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }
        [HttpGet("tasks-group/{GroupId}")]
        public async Task <IActionResult> GetTasksByGroup([FromRoute] Guid GroupId)
        {
            var supervisorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Tasks = await _taskService.GetTasksByGroup(GroupId ,supervisorId);
            if(Tasks is null)
            {
                return BadRequest(Tasks);
            }
            return Ok(new { message = "success", Tasks });
        }



        [HttpPatch("{GroupId}/tasks/{taskId}")]
        public async Task<IActionResult> UpdateTask([FromRoute] Guid GroupId, [FromRoute] Guid TaskId,[FromForm] TaskRequest Request)
        {
            var supervisorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _taskService.UpdateTask(supervisorId, GroupId, Request , TaskId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
