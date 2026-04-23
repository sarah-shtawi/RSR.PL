using RSR.DAL.Models.ProjectGroupModel;
using RSR.DAL.Models.TaskModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = RSR.DAL.Models.TaskModel.Task;

namespace RSR.DAL.Models.User
{
    public class SupervisorProfile : IUserProfile
    {
        [Key]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string SupervisorNumber { get; set; }
        public string Department { get; set; }
        public string? PictureProfileURL { get; set; }

        // relation with group 
        public List<Group> Groups { get; set; } = new List<Group>();

        // relation with Task 
        public List<Task> Tasks { get; set; } = new List<Task>();
    }
}
