using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogFinalProject.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public ApplicationUser User { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool AcceptedAnswer { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}