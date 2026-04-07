using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.GroupService;
using RSR.BLL.Service.Users;
using RSR.DAL.DTOs.Request.GroupRequest;
using RSR.DAL.DTOs.Request.UserRequest;
using RSR.DAL.Models.User;
using System.Security.Claims;

namespace RSR.PL.Areas.Supervisor
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Supervisor")]
    public class GroupController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;

        public GroupController(IUserService userService , IGroupService groupService)
        {
            _userService = userService;
            _groupService = groupService;
        }

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


        [HttpPost("create-group")]
        public async Task<IActionResult> CreateGroup([FromBody] GroupRequest request)
        {
            var SupervisorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _groupService.CreateGroup(request , SupervisorId);
            if (!result.Success) 
            {
                return BadRequest(result.Message);            
            }
            return Ok(result);

        }

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
    }
}
