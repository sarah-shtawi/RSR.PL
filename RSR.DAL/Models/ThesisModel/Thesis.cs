using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Models.ThesisModel
{
    public  class Thesis
    {

        public Guid ThesisId { get; set; } = Guid.NewGuid();
        public string ThesisFile { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeadLine { get; set; }


        // relation with Group 
        public Models.ProjectGroupModel.Group Group { get; set; }
        public Guid GroupId { get; set; }



        // relation with Thesis Version 
        public List<ThesisVersions> ThesisVersions { get; set; } = new();



    }
}
