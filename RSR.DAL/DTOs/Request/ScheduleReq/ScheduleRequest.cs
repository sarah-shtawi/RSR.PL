using RSR.DAL.Models.ScheduleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Request.ScheduleReq
{
    public class ScheduleRequest
    {
        public Guid GroupId { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string? Notes { get; set; }
        public List<string> ExaminersIds { get; set; }

    }
}
