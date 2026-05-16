using RSR.DAL.Models.ThesisModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RSR.DAL.DTOs.Request.ThesisReq
{
    public  class ReviewThesisRequest
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Decision Decision { get; set; }
        public string? Feedback { get; set; }
    }
}
