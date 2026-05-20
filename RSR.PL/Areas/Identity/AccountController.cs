using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.Authentication;
using RSR.DAL.DTOs.Request.Authentication;
using RSR.DAL.DTOs.Request.AuthenticationRequest;
using RSR.DAL.Models.User;
using System.Security.Claims;

namespace RSR.PL.Areas.Identity
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthenticationService _authenticationService;

        public AccountController(UserManager <ApplicationUser> userManager , IAuthenticationService authenticationService)
        {
            _userManager = userManager;
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authenticationService.Login(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpPost("send-code")]
        public async Task<IActionResult> SendCode([FromBody] ForgetPasswordRequest Request)
        {
            var result = await _authenticationService.SendCode(Request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("reset-password")]
        public async Task <IActionResult> ResetPassword([FromBody] ResetPasswordRequest Request)
        {
            var result = await _authenticationService.ResetPassword(Request);
            if (!result.Success) 
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest Request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _authenticationService.ChangePassword(Request, userId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenApiModel Request)
        {
            var result = await _authenticationService.RefreshToken(Request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
