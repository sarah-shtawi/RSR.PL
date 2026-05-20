using Microsoft.AspNetCore.Identity;
using RSR.BLL.Service.EmailSender;
using RSR.BLL.Service.Token;
using RSR.DAL.DTOs.Request.Authentication;
using RSR.DAL.DTOs.Request.AuthenticationRequest;
using RSR.DAL.DTOs.Response;
using RSR.DAL.DTOs.Response.AuthenticationResponse;
using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.Data;
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
        private readonly IEmailSenderService _emailSender;

        public AuthenticationService(UserManager <ApplicationUser> userManager , SignInManager <ApplicationUser> signInManager , ITokenService tokenService , IEmailSenderService emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailSender = emailSender;
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
                var roles = await _userManager.GetRolesAsync(user);

                var accessToken = await _tokenService.GeneraterAccessToken(user);
                var refreshToken = _tokenService.GenerateRefreshToken();
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

                user.RefreshToken = refreshToken;
                await _userManager.UpdateAsync(user);

                return new LoginResponse()
                {
                    Success = true,
                    Message = "Login Successfully",
                    AccessToken = accessToken ,
                    RefreshToken = refreshToken ,
                    roles = (List<string>)roles
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

        public async Task<BaseResponse> SendCode(ForgetPasswordRequest Request)
        {
            var user = await _userManager.FindByEmailAsync(Request.Email);
            if(user is null)
            {
                return new BaseResponse()
                {
                    Success =false , 
                    Message = "In Valied Email"
                };
            }
            Random random = new Random();
            var code = random.Next(1000, 9999).ToString();
            user.CodeResetPassword = code;
            user.PasswordResetCodeExpiry = DateTime.UtcNow.AddMinutes(15);

            await _userManager.UpdateAsync(user);
            await _emailSender.sendEmail(Request.Email , "forget password" ,$"<p> code is {code}</p>");

            return new BaseResponse()
            {
                Success= true,
                Message = "code sent to your email"
            };
        }

        public async Task<BaseResponse> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) 
            {
                return new BaseResponse
                {
                    Success = false ,
                    Message = "user is not found"
                };
            }
            else if (user.CodeResetPassword != request.code)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "code invalid"
                };
            }else if(user.PasswordResetCodeExpiry < DateTime.UtcNow)
            {
                return new BaseResponse()
                {
                    Success = false , 
                    Message = "code expired"
                };
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user , token , request.NewPassword);
            if (!result.Succeeded)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "reset passwprd Invalid",
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }
            user.CodeResetPassword = null;
            user.PasswordResetCodeExpiry = null;
            await _emailSender.sendEmail(request.Email , "Reset Password" , "<p> your password is changed successfully </p>");
            return new BaseResponse() 
            {
               Success = true ,
               Message = "Password Reset Successfully "
            };
        }

        public async Task<BaseResponse> ChangePassword(ChangePasswordRequest request , string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user is null)
            {
                return new BaseResponse()
                {
                    Success = false ,
                    Message = "user not found"
                };
            }
            if(request.NewPassword != request.ConfirmPassword) 
            {
                return new BaseResponse()
                {
                    Success= false ,
                    Message = "Passwords don't match"
                };
            }
            var result = await _userManager.ChangePasswordAsync(user , request.CurrentPassword , request.NewPassword);
            if (!result.Succeeded) 
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "Change Password failed ",
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }
            await _emailSender.sendEmail(user.Email, "Password Changed", "Your password has been changed successfully.");
            return new BaseResponse()
            {
                Success = true,
                Message = "Your Password is changed"
            };
        }

        public async Task<LoginResponse> RefreshToken(TokenApiModel request)
        {
            var accessToken = request.AccessToken;
            var refreshToken = request.RefreshToken;
            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            var UserName = principal.Identity.Name;
            var user = await _userManager.FindByNameAsync(UserName);

            if(user is null || refreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return new LoginResponse()
                {
                    Success = false ,
                    Message = "Invalid Client request"
                };
            }
            var newAccessToken = await _tokenService.GeneraterAccessToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            await _userManager.UpdateAsync(user);

            return new LoginResponse()
            {
                Success = true ,
                Message = "Token Refreshed", 
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            };
        }





    }
}
