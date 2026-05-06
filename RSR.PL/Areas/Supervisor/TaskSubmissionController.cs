using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.TaskSubmission;
using RSR.DAL.DTOs.Request.TaskReq;
using System.Security.Claims;

namespace RSR.PL.Areas.Supervisor
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles =("Supervisor"))]
    public class TaskSubmissionController : ControllerBase
    {
        private readonly ITaskSubmissionService _taskSubmissionService;

        public TaskSubmissionController(ITaskSubmissionService taskSubmissionService)
        {
            _taskSubmissionService = taskSubmissionService;
        }

        [HttpPost("Review/submissionId/{submisionId}")]
        public async Task<IActionResult> ReviewForSubmission([FromRoute] Guid submisionId , [FromBody] ReviewTaskSubmission request )
        {
            var supervisorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _taskSubmissionService.ReviewForSubmission(submisionId, supervisorId, request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
