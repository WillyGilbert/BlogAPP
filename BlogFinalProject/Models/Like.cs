using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogFinalProject.Models
{
    public class Like
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public ApplicationUser User { get; set; }
        public Nullable<int> QuestionId { get; set; }
        public Nullable<int> AnswerId { get; set; }
        public virtual Question Question { get; set; }
        public virtual Answer Answer { get; set; }
        public bool IsLiked { get; set; }
    }
}