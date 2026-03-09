using RSR.DAL.DTOs.Request.Authentication;
using RSR.DAL.DTOs.Request.AuthenticationRequest;
using RSR.DAL.DTOs.Response;
using RSR.DAL.DTOs.Response.AuthenticationResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.Service.Authentication
{
    public  interface  IAuthenticationService
    {
        Task<LoginResponse> Login(LoginRequest Request);
        Task<BaseResponse> SendCode(ForgetPasswordRequest Request);

        Task<BaseResponse> ResetPassword(ResetPasswordRequest request);
    }
}
