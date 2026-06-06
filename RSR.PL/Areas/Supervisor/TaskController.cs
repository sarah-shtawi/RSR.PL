using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.Task;
using RSR.DAL.DTOs.Request.TaskReq;
using RSR.DAL.DTOs.Response;
using RSR.DAL.Repository.GroupRepo;
using System.Security.Claims;

namespace RSR.PL.Areas.Supervisor
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IGroupRepository _groupRepository;

        public TaskController(ITaskService taskService , IGroupRepository groupRepository)
        {
            _taskService = taskService;
            _groupRepository = groupRepository;
        }
        [Authorize(Roles = "Supervisor")]
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
       
        
        [Authorize(Roles = "Supervisor,Student")]
        [HttpGet("tasks-group/{GroupId}")]
        public async Task <IActionResult> GetTasksByGroup([FromRoute] Guid GroupId)
        {
            var group = await _groupRepository.GroupByIdRepo(GroupId);

            if (group == null)
            {
                return NotFound(new BaseResponse
                {
                    Success = false,
                    Message = "Group not found"
                });
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);
            var Tasks = await _taskService.GetTasksByGroupForSupervisor(GroupId ,userId , role);
            if(Tasks is null)
            {
                return BadRequest(Tasks);
            }
            return Ok(new { message = "success", Tasks });
        }

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

    }
}
