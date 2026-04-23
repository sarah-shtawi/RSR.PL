using RSR.DAL.Models.TaskModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RSR.DAL.Models.User
{
    public class StudentProfile : IUserProfile
    {
        [Key]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string StudentNumber { get; set; }
        public string College {  get; set; }
        public string Major {  get; set; }
        public string? PictureProfileURL { get; set; }

        // relation with group 1 : M 
        public Guid? GroupId { get; set; }
        public ProjectGroupModel.Group Group { get; set; }
        // relation with task Submission 
        public List<TaskSubmission> TaskSubmissions { get; set; } = new();

    }
}
