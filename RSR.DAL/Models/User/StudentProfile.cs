using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
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

    }
}
