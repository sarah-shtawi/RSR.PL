using Microsoft.AspNetCore.Identity;
using RSR.DAL.Models.TaskModel;
using RSR.DAL.Models.ThesisModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Models.User
{
    public  class ApplicationUser :IdentityUser
    {

        public string FullName { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        // for reset Password 
        public string? CodeResetPassword { get; set; }
        public DateTime? PasswordResetCodeExpiry { get; set; }

        // for refresh token 
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        // relation with TaskSubmissionComments
        public List<TaskSubmissionComment> TaskSubmissionComments { get; set; } = new List<TaskSubmissionComment>();

        // relation with Thesis FeedBack 
        public List<ThesisFeedback> ThesisFeedbacks { get; set; } = new List<ThesisFeedback>();


        // Navigation Proparity with profiles 
        public StudentProfile? StudentProfile {  get; set; }
        public SupervisorProfile? SupervisorProfile { get; set; }
        public CoordinatorProfile? CoordinatorProfile { get; set; }
        public ExaminerProfile? ExaminerProfile { get; set; }
    }
}
