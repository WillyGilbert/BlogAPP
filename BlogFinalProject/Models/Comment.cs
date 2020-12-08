using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogFinalProject.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public ApplicationUser User { get; set; }
        public Nullable<int> QuestionId { get; set; }
        public Nullable<int> AnswerId { get; set; }
        public virtual Question Question { get; set; }
        public virtual Answer Answer { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}