using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.Users;
using RSR.DAL.DTOs.Request.UserRequest;
using RSR.DAL.Models.User;

namespace RSR.PL.Areas.Student
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Student")]
    public class StudentController : ControllerBase
    {
        private readonly IUserService _userService;
        public StudentController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("image-profile-student/{id}")]
        public async Task<IActionResult> AssignImageStudent([FromRoute] string id,  UploadImageRequest image)
        {
            var result = await _userService.AssignImage<StudentProfile>(image, id); ;
            if (!result.Success)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
