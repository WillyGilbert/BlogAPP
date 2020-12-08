using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogFinalProject.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<QuestionTag> Questions { get; set; }
    }
}