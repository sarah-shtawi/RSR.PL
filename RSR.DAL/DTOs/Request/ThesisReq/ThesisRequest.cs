using Microsoft.AspNetCore.Http;
using RSR.DAL.Models.ThesisModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Request.ThesisReq
{
    public  class ThesisRequest
    {
        public IFormFile ThesisFile { get; set; }
        public DateTime DeadLine { get; set; }

    }
}
