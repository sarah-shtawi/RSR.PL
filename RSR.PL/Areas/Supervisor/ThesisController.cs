using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.Semester;
using RSR.BLL.Service.Thesis;
using RSR.BLL.Service.ThesisVersions;
using RSR.DAL.DTOs.Request.ThesisReq;
using System;
using System.Security.Claims;

namespace RSR.PL.Areas.Supervisor
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThesisController : ControllerBase
    {
        private readonly IThesisService _thesisService;
        private readonly IThesisVersionsService _versionsService;
        private readonly ISemesterService _semesterService;

        public ThesisController(IThesisService thesisService , IThesisVersionsService versionsService , ISemesterService semesterService)
        {
            _thesisService = thesisService;
            _versionsService = versionsService;
            _semesterService = semesterService;
        }

        [Authorize(Roles = "Supervisor")]
        [HttpPost("create-thesis/group-Id/{GroupId}")]
        public async Task <IActionResult> CreateThesis([FromForm]ThesisRequest request ,[FromRoute] Guid GroupId)
        {
            var supervisorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _thesisService.CreateThesis(request , supervisorId , GroupId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [Authorize(Roles = "Supervisor")]
        [HttpPatch("update-thesis/Thesis-Id/{ThesisId}")]
        public async Task<IActionResult> UpdateThesis([FromForm] ThesisRequest request, [FromRoute] Guid ThesisId)
        {
            var supervisorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _thesisService.UpdateThesis(request, supervisorId, ThesisId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [Authorize(Roles = "Supervisor")]
        [HttpPost("Review-version/versionId/{VersionId}")]
        public async Task<IActionResult> ReviewVersionThesis([FromBody] ReviewThesisRequest request, [FromRoute] Guid VersionId)
        {
            var supervisorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _versionsService.ReviewThesisVersion( supervisorId, VersionId, request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }



        [Authorize(Roles = "Student,Supervisor")]
        [HttpGet("get-thesis/group-id/{groupId}")]
        public async Task<IActionResult> GetThesisByGroupId([FromRoute] Guid groupId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Role = User.FindFirstValue(ClaimTypes.Role);
            var result = await _thesisService.GetThesisByGroupId(groupId , userId , Role);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok( new { message = "success", result });
        }


        [Authorize(Roles = "Supervisor,Coordinator")]
        [HttpGet("projects-archive")]
        public async Task<IActionResult> ThesisArchive()
        {
            var projects = await _semesterService.ProjectForArchive();
            return Ok(new { message = "success", projects });
        }

        [Authorize(Roles = "Supervisor,Coordinator")]
        [HttpPost("publish-thesis/versionId/{versionId}")]
        public async Task<IActionResult> PublishThesis([FromRoute] Guid versionId)
        {
            var result = await _versionsService.PublishThesisVersion(versionId);
            if (!result.Success) 
            {
                return BadRequest(result);
            }
            return Ok(new { message = "success", result });
        }

        [HttpGet("thesis-homepage")]
        public async Task<IActionResult> ThesisHomePage()
        {
            var result = await _versionsService.GetThesisHomePage();      
            return Ok(new { message = "success", result });
        }


    }
}
