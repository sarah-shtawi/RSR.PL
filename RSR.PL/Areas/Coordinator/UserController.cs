using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.Users;
using RSR.DAL.DTOs.Request.UserRequest;

namespace RSR.PL.Areas.Coordinator
{
    [Route("api/[controller]")]
    [ApiController]
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
            var result  = await _userService.AssignStudent(Request);
            if (!result.Success) 
            {
            return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("AssignSupervisor")]
        public async Task<IActionResult> AssignSupervisor([FromForm] AssignSupervisorRequest Request)
        {
            var result = await _userService.AssignSupervisor(Request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("AssignCoordinater")]
        public async Task<IActionResult> AssignCoordinater([FromForm] AssignCoordinaterRequest Request)
        {
            var result = await _userService.AssignCoordinator(Request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("AssignExaminer")]
        public async Task<IActionResult> AssignExaminer([FromForm] AssignExaminerRequest Request)
        {
            var result = await _userService.AssignExaminer(Request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
