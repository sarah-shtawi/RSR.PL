using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.User
{
    public  class GetUserResponse
    {
        public string Id { get; set; }
        public string FullName { get; set; } = null!;
        public string UserName { get; set; }
        public string Email { get; set; }


    }
}
