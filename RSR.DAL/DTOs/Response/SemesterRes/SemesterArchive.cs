using RSR.DAL.DTOs.Response.ThesisRes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.SemesterRes
{
    public  class SemesterArchive
    {
        public Guid SemesterId { get; set; }

        public string Name { get; set; }

        public List<ThesisArchiveHomePageResponse> Projects { get; set; } 


    }
}
