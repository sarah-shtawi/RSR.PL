using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.Users;
using RSR.DAL.DTOs.Request.UserRequest;
using RSR.DAL.Models.User;
using System.Security.Claims;

namespace RSR.PL.Areas.Supervisor
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Supervisor")]
    public class SupervisorController : ControllerBase
    {
        private readonly IUserService _userService;
        public SupervisorController(IUserService userService)
        {
            _userService = userService;
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
    }
}
