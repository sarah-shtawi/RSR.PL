using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Request.UserRequest
{
    public  class AssignCoordinaterRequest : AssignUserRequest
    {
     
        public string CoordinatorNumber { get; set; }
        public string Department { get; set; }
    }
}
