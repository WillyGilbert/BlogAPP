using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogFinalProject.Models
{
    public class QuestionTagDetails
    {
        public int QuestionId { get; set; }
        public int TagId { get; set; }
        public string Description { get; set; }
    }
}