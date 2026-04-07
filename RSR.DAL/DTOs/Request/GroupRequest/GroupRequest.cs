using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Request.GroupRequest
{
    public  class GroupRequest
    {
        [Required(ErrorMessage = "The Group Name Is Required")]
        public string GroupName { get; set; }

        [Required(ErrorMessage = "The Project Idea Is Required")]
        public string ProjectIdea { get; set; }

        [Required(ErrorMessage = "The Project Name Is Required") ]
        public string ProjectName { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "The Students  Is Required")]
        public List<string> StudentIds { get; set; }
    }
}
