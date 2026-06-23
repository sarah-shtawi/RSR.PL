using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.Semester;
using RSR.DAL.DTOs.Request.semester;

namespace RSR.PL.Areas.Coordinator
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterController : ControllerBase
    {
        private readonly ISemesterService _semesterService;

        public SemesterController(ISemesterService semesterService)
        {
            _semesterService = semesterService;
        }

        [Authorize(Roles = "Coordinator")]
        [HttpPost("CreateSemester")]
        public async Task<IActionResult> CreateSemester([FromBody] CreateSemesterRequest request)
        {
            var result = await _semesterService.CreateSemester(request);
            if (!result.Success)
            {
                return BadRequest(new { message =  result.Message });
            }
            return Ok(result);
        }

        [Authorize(Roles = "Coordinator")]
        [HttpGet("ActiveSemester")]
        public async Task<IActionResult> GetActiveSemester()
        {
            var semester = await _semesterService.GetActiveSemester();
            return Ok(new { message = semester != null ? "success" : "no active semester", semester });
        }

        [Authorize(Roles = "Coordinator,Supervisor")]
        [HttpGet("AllSemesters")]
        public async Task<IActionResult> AllSemesters()
        {
            var semesters = await _semesterService.GetAllSemesters();
            if (semesters is null)
            {
                return BadRequest("Semesters Not Found");
            }
            return Ok(new { message="success" , semesters });
        }

        [Authorize(Roles = "Coordinator")]
        [HttpPatch("UpdateSemester/{Id}")]
        public async Task<IActionResult> UpdateSemester([FromRoute] Guid Id, [FromBody] CreateSemesterRequest request)
        {
            var result = await _semesterService.UpdateSemester(Id , request);
            if (!result.Success)
            {
                return BadRequest(new {message = result.Message });
            }
            return Ok(result);
        }

    }
}
