using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.Dashboared
{
    public  class UpComingDeadlineResponse
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
    }
}
