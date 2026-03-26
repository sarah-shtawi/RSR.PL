using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Request.UserRequest.update
{
    public  class UpdateSupervisorRequest : UpdateUserRequest
    {
        public string SupervisorNumber { get; set; }
        public string Department { get; set; }
    }
}
