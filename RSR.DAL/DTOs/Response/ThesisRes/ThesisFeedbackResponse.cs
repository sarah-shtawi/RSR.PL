using RSR.DAL.Models.ThesisModel;
using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Response.ThesisRes
{
    public  class ThesisFeedbackResponse
    {
        public Guid FeedbackId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Decision Decision { get; set; }
        public string? Feedback { get; set; }
        public DateTime CreateAt { get; set; }
        public string ReviwerId { get; set; }
        public string ReviwerName { get; set; }
    }
}
