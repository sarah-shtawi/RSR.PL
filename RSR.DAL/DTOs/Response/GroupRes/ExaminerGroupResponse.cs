using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.GroupRes
{
    public class ExaminerGroupResponse
    {
        public Guid GroupId { get; set; }

        public string GroupName { get; set; }

        public string ProjectName { get; set; } 

        public string ProjectIdea { get; set; }

        public string SupervisorName { get; set; }

        public List<StudentResponse> Students { get; set; }

    }
}
