using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.Dashboared
{
    public class CoordinatorDashboared
    {
        public int TotalUsers { get; set; }
        public int TotalProjects { get; set; }
        public int ActiveProjects { get; set; }
        public int Examinations { get; set; }

    }
}
