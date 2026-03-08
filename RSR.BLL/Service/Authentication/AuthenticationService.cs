using Microsoft.AspNetCore.Identity;
using RSR.BLL.Service.Token;
using RSR.DAL.DTOs.Request.Authentication;
using RSR.DAL.DTOs.Response.AuthenticationResponse;
using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.Service.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthenticationService(UserManager <ApplicationUser> userManager , SignInManager <ApplicationUser> signInManager , ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        public async Task<LoginResponse> Login(LoginRequest Request)
        {
            try{
                var user = await _userManager.FindByEmailAsync(Request.Email);
                if (user is null)
                {
                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "In Valied Email"
                    };
                }
                if (await _userManager.IsLockedOutAsync(user))
                {
                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "Your Account is Locked , try again later "
                    };
                }
                var result = await _signInManager.CheckPasswordSignInAsync(user, Request.Password, true);

                if (result.IsLockedOut)
                {
                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "Account Locked due to multiple falied attempts"
                    };
                }
                if (!result.Succeeded)
                {
                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "InValied Password"
                    };
                }
                var accessToken = await _tokenService.GeneraterAccessToken(user);
                return new LoginResponse()
                {
                    Success = true,
                    Message = "Login Successfully",
                    AccessToken = accessToken
                };
            }     
            catch(Exception ex) {
                return new LoginResponse()
                {
                    Success = false,
                    Message = "An unexpected error",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}
