using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Request.EvaluationRequest
{
    public class UpdateEvaluationFormRequest
    {
        public string Title { get; set; } = string.Empty;

        public string AssignTo { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}