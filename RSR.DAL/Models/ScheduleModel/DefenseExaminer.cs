using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Models.ScheduleModel
{
    public  class DefenseExaminer
    {
        public Guid DefenseExaminerId { get; set; } = Guid.NewGuid();


        // relation with Schedule
        public Guid ScheduleId { get; set; }
        public Schedule Schedule { get; set; }


        // relation with examiner 
        public string ExaminerId { get; set; }
        public ExaminerProfile Examiner { get; set; }
    }
}
