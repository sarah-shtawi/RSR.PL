using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Models.User
{
    public class SupervisorProfile
    {
        [Key]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string SupervisorNumber { get; set; }
        public string Department { get; set; }

    }
}
