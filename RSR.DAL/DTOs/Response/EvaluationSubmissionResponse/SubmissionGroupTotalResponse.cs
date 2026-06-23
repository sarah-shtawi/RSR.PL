using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.EvaluationSubmissionResponse
{
    public  class SubmissionGroupTotalResponse
    {

        public Guid GroupId { get; set; }
        public float Total { get; set; }
    }
}
