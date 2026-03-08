using RSR.DAL.DTOs.Request.Authentication;
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
       public Task<LoginResponse> Login(LoginRequest Request);
    }
}
