using RSR.DAL.Models.ProjectModel;
using RSR.DAL.Models.SemesterModel;
using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Models.ProjectGroupModel
{
    public class Group
    {
        public Guid GroupId { get; set; } = Guid.NewGuid();
        public string GroupName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // relation with project 1 : 1 
        public Project Project { get; set; }

        // relation with student profile  1 : M 
        public List<StudentProfile> Students { get; set; }

        // relation with supervisor 
        public string SupervisorId { get; set; }
        public SupervisorProfile Supervisor { get; set; }

        // relation with semester 
        public Guid SemesterId { get; set; }
        public Semester Semester { get; set; }
    }
}
