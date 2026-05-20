using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Request.EvaluationRequest
{
    public class UpdateEvaluationFieldRequest
    {
        public string FieldName { get; set; } = string.Empty;

        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        public bool IsRequired { get; set; }
    }
}