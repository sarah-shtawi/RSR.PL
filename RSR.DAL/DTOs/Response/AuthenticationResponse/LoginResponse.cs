using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.AuthenticationResponse
{
    public  class LoginResponse : BaseResponse
    {
        public string AccessToken { get; set; }

    }
}
