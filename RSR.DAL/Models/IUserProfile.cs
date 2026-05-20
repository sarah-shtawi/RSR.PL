using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Models
{
    public  interface IUserProfile
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string? PictureProfileURL { get; set; }
    }
}
