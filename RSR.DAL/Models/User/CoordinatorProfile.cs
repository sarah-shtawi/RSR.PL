using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Models.User
{
    public class CoordinatorProfile : IUserProfile
    {

        [Key]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string CoordinatorNumber { get; set; }
        public string Department {  get; set; }
        public string? PictureProfileURL { get; set; }
    }
}
