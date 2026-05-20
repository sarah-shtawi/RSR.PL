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
<<<<<<< HEAD
    [Authorize(Roles ="Supervisor")]
=======
>>>>>>> origin/master
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
<<<<<<< HEAD

=======
        [Authorize(Roles = "Supervisor")]
>>>>>>> origin/master
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
<<<<<<< HEAD
        [HttpGet("tasks-group/{GroupId}")]
        public async Task <IActionResult> GetTasksByGroup([FromRoute] Guid GroupId)
        {
            var supervisorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Tasks = await _taskService.GetTasksByGroup(GroupId ,supervisorId);
=======
       
        
        [Authorize(Roles = "Supervisor,Student")]
        [HttpGet("tasks-group/{GroupId}")]
        public async Task <IActionResult> GetTasksByGroup([FromRoute] Guid GroupId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);
            var Tasks = await _taskService.GetTasksByGroupForSupervisor(GroupId ,userId , role);
>>>>>>> origin/master
            if(Tasks is null)
            {
                return BadRequest(Tasks);
            }
            return Ok(new { message = "success", Tasks });
        }

<<<<<<< HEAD


=======
        [Authorize(Roles = ("Supervisor,Student"))]
        [HttpGet("task-id/{TaskId}")]
        public async Task<IActionResult> GetTaskDetails([FromRoute] Guid TaskId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);
            var task = await _taskService.TaskDetails(TaskId, userId, role);
            if(task is null)
            {
                return BadRequest(task);
            }
            return Ok(new { message = "success", task });

        }


        [Authorize(Roles = "Supervisor")]
>>>>>>> origin/master
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

<<<<<<< HEAD
=======
        [Authorize(Roles = "Supervisor")]
        [HttpDelete("remove-delete/task-id/{taskId}")]
        public async Task <IActionResult> RemoveTask([FromRoute] Guid taskId)
        {
            var supervisorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _taskService.DeleteTask(taskId , supervisorId);
            if (!result.Success) 
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

>>>>>>> origin/master
    }
}
