using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.GroupRes
{
    public class GetAllSupervisorsWithGroups
    {
        public string SupervisorName { get; set; }
        public List<GroupResponse> Groups { get; set; }
    }
}
