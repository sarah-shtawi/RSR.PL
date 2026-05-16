using RSR.DAL.DTOs.Response.GroupRes;
using RSR.DAL.Models.ThesisModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.ThesisRes
{
    public  class ThesisResponse : BaseResponse
    {
        public Guid ThesisId { get; set; } 
        public string ThesisFile { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeadLine { get; set; }
        public string SupervisorName { get; set; }
        public List<ThesisVersionResponse> ThesisVersions { get; set; }
    }
}
