using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.Users;
using RSR.DAL.DTOs.Request.UserRequest;

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

        [HttpGet("students")]
        public async Task<IActionResult> getStudent()
        {
            var students = await _userService.GetStudents();
            if (students == null)
            {
                return BadRequest();
            }
            return Ok( new { message = "success", students } );
        }

        [HttpGet("coordinaters")]
        public async Task<IActionResult> getCoordinaters()
        {
            var coordinaters = await _userService.GetCoordinators();
            if (coordinaters == null)
            {
                return BadRequest();
            }
            return Ok(new { message = "success", coordinaters });
        }
        [HttpGet("supervisors")]
        public async Task<IActionResult> getSupervisors()
        {
            var supervisors = await _userService.GetSupervisors();
            if (supervisors == null)
            {
                return BadRequest();
            }
            return Ok(new { message = "success", supervisors });
        }

        [HttpGet("examiners")]
        public async Task<IActionResult> getExaminers()
        {
            var examiners = await _userService.GetExaminers();
            if (examiners == null)
            {
                return BadRequest();
            }
            return Ok(new { message = "success", examiners });
        }

        [HttpGet("student/{Id}")]
        public async Task<IActionResult> getStudentById([FromRoute] string id)
        {
            var student = await _userService.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(new { message = "success", student });

        }
        [HttpGet("supervisor/{Id}")]
        public async Task<IActionResult> getSupervisorById([FromRoute] string id)
        {
            var supervisor = await _userService.GetSupervisorById(id);
            if (supervisor == null)
            {
                return NotFound();
            }
            return Ok(new { message = "success", supervisor });
        }
        [HttpGet("coordinater/{Id}")]
        public async Task<IActionResult> getCoordinater([FromRoute] string id)
        {
            var coordinater = await _userService.GetCoordinaterById(id);
            if (coordinater == null)
            {
                return NotFound();
            }
            return Ok(new { message = "success", coordinater });
        }
        [HttpGet("examiner/{Id}")]
        public async Task<IActionResult> getExaminer([FromRoute] string id)
        {
            var Examiner = await _userService.GetExaminerById(id);
            if (Examiner == null)
            {
                return NotFound();
            }
            return Ok(new { message = "success", Examiner });
        }
    }
    }
