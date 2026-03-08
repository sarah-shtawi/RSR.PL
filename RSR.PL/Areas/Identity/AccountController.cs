using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.Authentication;
using RSR.DAL.DTOs.Request.Authentication;
using RSR.DAL.Models.User;

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
    }
}
