using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Request.UserRequest.update
{
    public  class UpdateCoordinaterRequest : UpdateUserRequest
    {
        public string CoordinatorNumber { get; set; }
        public string Department { get; set; }
    }
}
