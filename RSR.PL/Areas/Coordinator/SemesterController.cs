using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.Semester;
using RSR.DAL.DTOs.Request.semester;

namespace RSR.PL.Areas.Coordinator
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Coordinator")]
    public class SemesterController : ControllerBase
    {
        private readonly ISemesterService _semesterService;

        public SemesterController(ISemesterService semesterService)
        {
            _semesterService = semesterService;
        }

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

        [HttpGet("ActiveSemester")]
        public async Task<IActionResult> GetActiveSemester()
        {
            var semester = await _semesterService.GetActiveSemester();
            if (semester is null)
            {
                return BadRequest("Semester Not Found");
            }
            return Ok(new {message="success" , semester });
        }

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

        [HttpPatch("UpdateSemester/{Id}")]
        public async Task <IActionResult> updateSemester([FromRoute] Guid Id , [FromBody] CreateSemesterRequest request)
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
