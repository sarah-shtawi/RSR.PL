using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Models.ThesisModel
{
    public enum Decision
    {
        Approved = 1, Rejected = 2
    }
    public  class ThesisFeedback
    {
        [Key]
        public Guid FeedbackId { get; set; } = Guid.NewGuid();
        public Decision Decision { get; set; }
        public string? Feedback { get; set; }
        public DateTime CreateAt { get; set; }

        // relation with Versions 
        public Guid VersionId { get; set; }
        public ThesisVersions ThesisVersion { get; set; }


        // relation with User [ Supervisor , Examiner ] 
        public ApplicationUser Reviwer { get; set; }
        public string ReviwerId { get; set; }
    }
}
