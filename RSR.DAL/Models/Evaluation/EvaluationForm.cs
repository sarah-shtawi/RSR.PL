using RSR.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Models.Evaluation
{
    public class EvaluationForm
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string AssignTo { get; set; } = string.Empty;

        public string? Description { get; set; }
        //Form status

        public FormStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

 
        // One-to-Many relation
        public ICollection<EvaluationField> Fields { get; set; }
            = new List<EvaluationField>();
    }
}