using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Request.TaskReq
{
    public  class ReplyToCommentRequest
    {
        [Required(ErrorMessage ="Content is Required")]
        [MaxLength(100)]
        public string Content { get; set; }
    }
}
