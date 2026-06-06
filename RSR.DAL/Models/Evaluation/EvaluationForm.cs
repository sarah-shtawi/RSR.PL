using RSR.DAL.Enums;
using RSR.DAL.Models.SemesterModel;

namespace RSR.DAL.Models.Evaluation
{
    public class EvaluationForm
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string AssignTo { get; set; } = string.Empty;
        public string? Description { get; set; }
        public FormStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // SEMESTER RELATION
        public Guid SemesterId { get; set; }
        public Semester Semester { get; set; }

        // FIELDS
        public ICollection<EvaluationField> Fields { get; set; }
            = new List<EvaluationField>();
    }
}