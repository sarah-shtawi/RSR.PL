using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Request.ThesisReq
{
    public  class ThesisVersionRequest
    {

        [Required(ErrorMessage ="Thesis File Is Required")]
        public IFormFile? ThesisVersionFile { get; set; }

    }
}
