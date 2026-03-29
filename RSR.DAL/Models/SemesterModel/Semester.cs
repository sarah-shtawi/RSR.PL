using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Models.SemesterModel
{
    public class Semester
    {
        public Guid SemesterId { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; } 
    }
}
