using RSR.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Request.EvaluationRequest
{
    public class CreateEvaluationFormRequest
    {
        public string Title { get; set; }

        public string AssignTo { get; set; }

        public string? Description { get; set; }

        public FormStatus Status { get; set; }
        public List<CreateEvaluationFieldRequest> Fields { get; set; }
                    = new();
    }
}