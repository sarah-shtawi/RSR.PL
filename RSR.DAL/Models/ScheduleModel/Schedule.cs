using RSR.DAL.Models.ProjectGroupModel;
using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Models.ScheduleModel
{
    public  class Schedule
    {
        public Guid ScheduleId { get; set; } = Guid.NewGuid();

        // relation with group 
        public Guid GroupId { get; set; }
        public Group Group { get; set; }

        // relation with User [ coordinator ] 
        public string CoordinatorId { get; set; }
        public CoordinatorProfile Coordinator { get; set; }

        // information 
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string? Notes { get; set; }

        // relation with Defense Examiners
        public List<DefenseExaminer> DefenseExaminers { get; set; }
    }
}
