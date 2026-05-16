using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Models.ThesisModel
{
    public  class ThesisVersions
    {
        [Key]
        public Guid VersionId { get; set; } = Guid.NewGuid();
        public string FileURL { get; set; }
        public int VersionNumber { get; set; }
        public bool IsLatest { get; set; }
        public bool IsFrozen { get; set; }
        public bool VisibleByExaminer { get; set; }
        public DateTime UploadedAt { get; set; }
        public bool IsPublished {  get; set; }
        public DateTime? PublishedAt { get; set; }

        // relation with student 
        public string studentId { get; set; }
        public StudentProfile student { get; set; }


        // relation with Thesis 
        public Guid ThesisId { get; set; }
        public Thesis Thesis { get; set; }


        // relation with Thesis FeedBack 
        public List<ThesisFeedback> thesisFeedbacks { get; set; }
    }
}
