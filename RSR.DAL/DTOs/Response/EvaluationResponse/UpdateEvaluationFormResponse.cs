using RSR.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.EvaluationResponse
{
    public class UpdateEvaluationFormResponse
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string AssignTo { get; set; } = string.Empty;

        public string? Description { get; set; }

        public FormStatus Status { get; set; }
    }
}