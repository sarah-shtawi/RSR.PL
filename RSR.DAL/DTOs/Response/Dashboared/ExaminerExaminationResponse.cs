
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.Dashboared
{
    public  class ExaminerExaminationResponse
    {
        public Guid ScheduleId { get; set; }
        public string ProjectName { get; set; }
        public string GroupName { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
    }
}
