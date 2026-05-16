using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.Thesis;
using RSR.BLL.Service.ThesisVersions;
using RSR.DAL.DTOs.Request.ThesisReq;
using System.Security.Claims;

namespace RSR.PL.Areas.Student
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Student")]
    public class ThesisVersionsController : ControllerBase
    {
        private readonly IThesisVersionsService _versionsService;

        public ThesisVersionsController(IThesisVersionsService versionsService)
        {
            _versionsService = versionsService;
        }
        [HttpPost("add-version/thesis-Id/{thesisId}")]
        public async Task <IActionResult> addVersionForThesis([FromRoute] Guid thesisId , [FromForm] ThesisVersionRequest request  )
        {
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _versionsService.AddThesisVersion(request, studentId, thesisId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpPut("Update-version/thesis-version-Id/{thesisVersionId}")]
        public async Task<IActionResult> UpdateThesisVersion([FromRoute] Guid thesisVersionId, [FromForm] ThesisVersionRequest request)
        {
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _versionsService.UpdateThesisVersion(request, studentId, thesisVersionId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
