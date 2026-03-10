using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Request.AuthenticationRequest
{
    public  class ChangePasswordRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
