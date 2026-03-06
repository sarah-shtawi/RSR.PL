using Microsoft.AspNetCore.Identity;
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

        public string  FullName { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateAt { get; set; } = DateTime.UtcNow;


       // Navigation Proparity 
        public StudentProfile StudentProfile {  get; set; }
        public SupervisorProfile SupervisorProfile { get; set; }
        public CoordinatorProfile CoordinatorProfile { get; set; }
        public ExaminerProfile ExaminerProfile { get; set; }
    }
}
