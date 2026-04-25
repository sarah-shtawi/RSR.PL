using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Request.TaskReq
{
    public  class TaskRequest
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(100)]
        public string Description { get; set; }
        public string? SupervisorNotes { get; set; }
        public IFormFile? TaskFileURL { get; set; }
        
        [Required(ErrorMessage = "DeadLine is required")]
        public DateTime DeadLine { get; set; }
    }
}
