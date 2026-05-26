using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response
{
    public class SupervisorFinalGradeResponse
    {
        public Guid GroupId { get; set; }

        public string GroupName { get; set; }

        public double FinalGrade { get; set; }
    }
}