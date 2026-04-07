using RSR.DAL.Models.ProjectGroupModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RSR.DAL.Models.ProjectModel
{
    public enum Status
    {
        InProgress = 1 , Completed = 2
    }
    public  class Project
    {
        public Guid ProjectId { get; set; } = Guid.NewGuid();
        public string ProjectIdea { get; set; }
        public string ProjectName { get; set; }
        public string? Description  { get; set; }
        public  Status ProjectStatus { get; set; }
        // relaion with Group 
        public Guid GroupId { get; set; }
        public  ProjectGroupModel.Group  Group { get; set; }
    }
}
