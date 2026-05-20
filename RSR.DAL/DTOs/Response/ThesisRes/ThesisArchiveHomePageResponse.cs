using RSR.DAL.Models.ProjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.ThesisRes
{
    public class ThesisArchiveHomePageResponse
    {
        public Guid ThesisVersionId {  get; set; }
        public string ProjectIdea { get; set; }
        public string ProjectName { get; set; }
        public string ThesisFile { get; set; }
        public DateTime? PublishedAt { get; set; }
    }
}
