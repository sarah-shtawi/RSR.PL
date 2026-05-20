using RSR.DAL.Models.ThesisModel;
using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.ThesisRes
{
    public  class ThesisVersionResponse
    {
        public Guid VersionId { get; set; } 
        public string FileURL { get; set; }
        public int VersionNumber { get; set; }
        public bool IsFrozen { get; set; }
        public DateTime UploadedAt { get; set; }
        public string studentId { get; set; }
        public string studentName { get; set; }
        public List<ThesisFeedbackResponse> thesisFeedbacks { get; set; }
    }
}
