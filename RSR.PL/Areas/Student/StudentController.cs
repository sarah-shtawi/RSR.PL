using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.EvaluationService;
using RSR.BLL.Service.Users;
using RSR.DAL.DTOs.Request.UserRequest;
using RSR.DAL.Models.User;
using System.Security.Claims;

namespace RSR.PL.Areas.Student
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Student")]
    public class StudentController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IFinalEvaluationResultService
            _finalResultService;

        public StudentController(
            IUserService userService,
            IFinalEvaluationResultService finalResultService)
        {
            _userService = userService;

            _finalResultService = finalResultService;
        }

         // UPLOAD PROFILE IMAGEeeee
         [HttpPost("image-profile-student")]
        public async Task<IActionResult>
            AssignImageStudent(
            [FromForm] UploadImageRequest image)
        {
            var userId =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

            var result =
                await _userService
                    .AssignImage<StudentProfile>(
                        image,
                        userId);

            if (!result.Success)
            {
                return BadRequest();
            }

            return Ok(result);
        }

         // GET FINAL GRADE
         [HttpGet("final-grade/{groupId}")]
        public async Task<IActionResult>
            GetFinalGrade(Guid groupId)
        {
            try
            {
                var result =
                    await _finalResultService
                        .GetStudentFinalGradeAsync(groupId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}