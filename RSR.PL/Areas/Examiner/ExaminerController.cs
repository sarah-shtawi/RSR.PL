using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.Users;
using RSR.DAL.DTOs.Request.UserRequest;
using RSR.DAL.Models.User;

namespace RSR.PL.Areas.Examiner
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Examiner")]
    public class ExaminerController : ControllerBase
    {
        private readonly IUserService _userService;
        public ExaminerController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("image-profile-examiner/{id}")]
        public async Task<IActionResult> AssignImageExaminer([FromRoute] string id, UploadImageRequest image)
        {
            var result = await _userService.AssignImage<ExaminerProfile>(image, id); ;
            if (!result.Success)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
