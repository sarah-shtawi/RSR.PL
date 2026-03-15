using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response
{
    public  class ErrorHandling
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }
    }
}
