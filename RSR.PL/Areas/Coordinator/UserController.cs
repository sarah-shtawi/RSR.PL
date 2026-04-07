using Azure.Core;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.Users;
using RSR.DAL.DTOs.Request.UserRequest;
using RSR.DAL.DTOs.Request.UserRequest.update;
using RSR.DAL.DTOs.Response.User;
using RSR.DAL.Models.User;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace RSR.PL.Areas.Coordinator
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Coordinator")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("AssignStudent")]
        public async Task <IActionResult> AssignStudent([FromForm] AssignStudentRequest Request)
        {
            var result =  await _userService.AssignUserWithProfile<StudentProfile>(Request, "Student");
            ;
            if (!result.Success) 
            {
            return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("AssignSupervisor")]
        public async Task<IActionResult> AssignSupervisor([FromForm] AssignSupervisorRequest Request)
        {
            var result = await _userService.AssignUserWithProfile<SupervisorProfile>(Request, "Supervisor");
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("AssignCoordinater")]
        public async Task<IActionResult> AssignCoordinater([FromForm] AssignCoordinaterRequest Request)
        {
            var result = await _userService.AssignUserWithProfile<CoordinatorProfile>(Request, "Coordinator");

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("AssignExaminer")]
        public async Task<IActionResult> AssignExaminer([FromForm] AssignExaminerRequest Request)
        {
            var result = await _userService.AssignUserWithProfile<ExaminerProfile>(Request, "Examiner"); ;
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("students")]
        public async Task<IActionResult> getStudent()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var students = await _userService.GetAllUsersWithProfile<StudentProfile, StudentGetResponse>(); ;
            if (students == null)
            {
                return BadRequest();
            }
            return Ok( new { message = "success", students } );
        }

        [HttpGet("coordinaters")]
        public async Task<IActionResult> getCoordinaters()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var coordinaters = await _userService.GetAllUsersWithProfile<CoordinatorProfile, CoordinatorGetResponse>();
            ;
            if (coordinaters == null)
            {
                return BadRequest();
            }
            return Ok(new { message = "success", coordinaters });
        }
        [HttpGet("supervisors")]
        public async Task<IActionResult> getSupervisors()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var supervisors = await _userService.GetAllUsersWithProfile<SupervisorProfile, SupervisorGetResponse>();
            if (supervisors == null)
            {
                return BadRequest();
            }
            return Ok(new { message = "success", supervisors });
        }

        [HttpGet("examiners")]
        public async Task<IActionResult> getExaminers()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var examiners = await _userService.GetAllUsersWithProfile<ExaminerProfile, ExaminerGetResponse>();
            if (examiners == null)
            {
                return BadRequest();
            }
            return Ok(new { message = "success", examiners });
        }

        [HttpGet("student/{Id}")]
        public async Task<IActionResult> getStudentById([FromRoute] string id)
        {
            var student = await _userService.GetUserById<StudentProfile, StudentGetResponse>(id); ;
            if (student == null)
            {
                return NotFound();
            }
            return Ok(new { message = "success", student });

        }

        [HttpGet("supervisor/{Id}")]
        public async Task<IActionResult> getSupervisorById([FromRoute] string id)
        {
            var supervisor = await _userService.GetUserById<SupervisorProfile, SupervisorGetResponse>(id);
            if (supervisor == null)
            {
                return NotFound();
            }
            return Ok(new { message = "success", supervisor });
        }

        [HttpGet("coordinater/{Id}")]
        public async Task<IActionResult> getCoordinater([FromRoute] string id)
        {
            var coordinater = await _userService.GetUserById<CoordinatorProfile, CoordinatorGetResponse>(id);
            if (coordinater == null)
            {
                return NotFound();
            }
            return Ok(new { message = "success", coordinater });
        }

        [HttpGet("examiner/{Id}")]
        public async Task<IActionResult> getExaminer([FromRoute] string id)
        {
            var Examiner = await _userService.GetUserById<ExaminerProfile, ExaminerGetResponse>(id); ;
            if (Examiner == null)
            {
                return NotFound();
            }
            return Ok(new { message = "success", Examiner });
        }

        [HttpPost("image-profile-coordinater")]
        public async Task<IActionResult> AssignImageCoordinator([FromForm] UploadImageRequest image)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _userService.AssignImage<CoordinatorProfile>(image, userId); ;
            if (!result.Success)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPatch("update-student/{studentId}")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentRequest request , [FromRoute] string studentId)
        {
            var result = await _userService.UpdateUserWithProfile(studentId, request);
            if (!result.Success) 
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpPatch("update-supervisor/{supervisorId}")]
        public async Task<IActionResult> UpdateSupervisor([FromBody] UpdateSupervisorRequest request, [FromRoute] string supervisorId)
        {
            var result = await _userService.UpdateUserWithProfile(supervisorId, request);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpPatch("update-examiner/{examinerId}")]
        public async Task<IActionResult> UpdateExaminer([FromBody] UpdateExaminerRequest request, [FromRoute] string examinerId)
        {
            var result = await _userService.UpdateUserWithProfile(examinerId, request);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpPatch("update-coordinater/{coordinaterId}")]
        public async Task<IActionResult> UpdateCoordinater([FromBody] UpdateCoordinaterRequest request, [FromRoute] string coordinaterId)
        {
            var result = await _userService.UpdateUserWithProfile(coordinaterId, request);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPatch("block/{userId}")]
        public async Task <IActionResult> BlockUser([FromRoute] string userId)
        {
            var result = await _userService.BlockUser(userId);
            if (!result.Success) 
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPatch("unblock/{userId}")]
        public async Task<IActionResult> unBlockUser([FromRoute] string userId)
        {
            var result = await _userService.unBlockUser(userId);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }





    }
    }
