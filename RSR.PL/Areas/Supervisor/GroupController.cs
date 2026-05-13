using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.GroupService;
using RSR.BLL.Service.Users;
using RSR.DAL.DTOs.Request.GroupRequest;
using RSR.DAL.DTOs.Request.UserRequest;
using RSR.DAL.DTOs.Response.User;
using RSR.DAL.Models.User;
using System.Security.Claims;

namespace RSR.PL.Areas.Supervisor
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;

        public GroupController(IUserService userService , IGroupService groupService)
        {
            _userService = userService;
            _groupService = groupService;
        }
        [Authorize(Roles = "Supervisor")]
        [HttpGet("students-supervisor")]
        public async Task<IActionResult> getStudent()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var students = await _userService.GetAllUsersWithProfile<StudentProfile, StudentGetResponse>(); ;
            if (students == null)
            {
                return BadRequest();
            }
            return Ok(new { message = "success", students });
        }

        [Authorize(Roles = "Supervisor")]
        [HttpPost("image-profile-supervisor")]
        public async Task<IActionResult> AssignImageSupervisor( [FromForm] UploadImageRequest image)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _userService.AssignImage<SupervisorProfile>(image, userId); ;
            if (!result.Success)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [Authorize(Roles = "Supervisor")]
        [HttpGet("groups-supervisor")]
        public async Task<IActionResult> GetSupervisorGroups()
        {
            var SupervisorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _groupService.GetSupervisorGroups(SupervisorId);
            if(result is null)
            {
                return BadRequest();
            }
            return Ok( new { message = "success" , groups =  result });
        }

        [Authorize(Roles = "Supervisor")]
        [HttpPost("create-group")]
        public async Task<IActionResult> CreateGroup([FromBody] GroupRequest request)
        {
            var SupervisorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _groupService.CreateGroup(request , SupervisorId);
            if (!result.Success) 
            {
                return BadRequest(result);            
            }
            return Ok(result);

        }

        [Authorize(Roles = "Supervisor")]
        [HttpPatch("update-group/{GroupId}")]
        public async Task <IActionResult> UpdateGroup([FromRoute] Guid GroupId , [FromBody] GroupRequest request)
        {
            var SupervisorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _groupService.UpdateGroup(request , SupervisorId , GroupId);
            if (!result.Success) 
            {
              return BadRequest(new {success = false , Message = result.Message});
            }
            return Ok(result);
        }


        [Authorize(Roles = "Student")]
        [HttpGet("my-group/{studentId}")]
        public async Task <IActionResult> GetGroupByStudent([FromRoute] string studentId)
        {
            var group = await _groupService.GetGroupByStudent(studentId);
            if(group is null)
            {
                return NotFound();
            }
            return Ok(group);
        }
    }
}
