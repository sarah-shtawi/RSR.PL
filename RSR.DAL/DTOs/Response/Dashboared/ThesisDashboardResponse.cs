using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.ThesisRes
{
    public  class ThesisDashboardResponse
    {
        public Guid ThesisId { get; set; }
        public Guid ThesisVersionId {  get; set; }
        public Guid GroupId { get; set; }
        public string ProjectName { get; set; }
        public string GroupName { get; set; }
        public DateTime UploadedAt { get; set; }

    }
}
