using RSR.DAL.Models.Evaluation;
using RSR.DAL.Models.ProjectGroupModel;

namespace RSR.DAL.Models.SemesterModel
{
    public class Semester
    {
        public Guid SemesterId { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        public List<Group> Groups { get; set; }

        // EVALUATION FORMS
        public List<EvaluationForm> EvaluationForms { get; set; } = new();
    }
}