using RSR.DAL.Enums;
using System;
using System.Collections.Generic;

namespace RSR.DAL.DTOs.Response.EvaluationResponse
{
    public class CreateEvaluationFormResponse
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string AssignTo { get; set; } = string.Empty;

        public string? Description { get; set; }

        public FormStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<CreateEvaluationFieldResponse> Fields { get; set; }
            = new();
    }
}