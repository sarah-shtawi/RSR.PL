using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.TaskRes
{
<<<<<<< HEAD
    public  class TaskResponse
=======
    public  class TaskResponse 
>>>>>>> origin/master
    {
        public Guid TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? SupervisorNotes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeadLine { get; set; }
        public string? TaskFileURL { get; set; }
<<<<<<< HEAD
=======
        public string SupervisorName { get; set; }
>>>>>>> origin/master
    }
}
