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
    public class TaskSubmissionController : ControllerBase
    {
        private readonly ITaskSubmissionService _taskSubmissionService;

        public TaskSubmissionController(ITaskSubmissionService taskSubmissionService)
        {
            _taskSubmissionService = taskSubmissionService;
        }
        [Authorize(Roles = ("Supervisor"))]
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
        [Authorize(Roles = ("Supervisor,Student"))]
        [HttpPost("reply-to-comment/parentCommentId/{parentCommentId}")]
        public async Task <IActionResult> ReplyToComment([FromBody] ReplyToCommentRequest request , [FromRoute] Guid parentCommentId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _taskSubmissionService.ReplyToComment(userId, parentCommentId, request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


    }
}
