using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.User
{
    public  class SupervisorGetResponse : GetUserResponse
    {
        public string SupervisorNumber { get; set; }
        public string Department { get; set; }
        public string? PictureProfileURL { get; set; }
    }
}
