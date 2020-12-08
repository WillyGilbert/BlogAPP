using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogFinalProject.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using PagedList;

namespace BlogFinalProject.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        [Authorize]
        // GET: Questions
        public ActionResult Index(int? page, bool? sortedByAnswer)
        {
            var query = from tag in db.Tags.ToList()
                        join questionTag in db.QuestionTags.ToList() on tag equals questionTag.Tag
                        select new QuestionTagDetails()
                        {
                            QuestionId = questionTag.QuestionId,
                            TagId = questionTag.TagId,
                            Description = tag.Description
                        };

            ViewBag.Tags = query.ToList();

            string UserId = User.Identity.GetUserId();
            ViewBag.Likes = db.Likes.Where(U => U.UserId == UserId).ToList();

            ViewBag.numberOfLikes = db.Likes.Where(z => z.IsLiked == true && z.AnswerId == null)
                .GroupBy(l => l.QuestionId)
                .Select(group => new LikeDetails() { QuestionId = (int)group.Key, Count = group.Count() })
                .OrderBy(x => x.QuestionId).ToList();

            ViewBag.numberOfDisLikes = db.Likes.Where(z => z.IsLiked == false && z.AnswerId == null)
                .GroupBy(l => l.QuestionId)
                .Select(group => new LikeDetails() { QuestionId = (int)group.Key, Count = group.Count() })
                .OrderBy(x => x.QuestionId).ToList();

            ViewBag.numberOfLikesOnAnswer = db.Likes.Where(z => z.IsLiked == true && z.AnswerId != null)
                .GroupBy(l => l.QuestionId)
                .Select(group => new LikeDetails() { QuestionId = (int)group.Key, Count = group.Count() })
                .OrderBy(x => x.QuestionId).ToList();

            ViewBag.numberOfDisLikesOnAnswer = db.Likes.Where(z => z.IsLiked == false && z.AnswerId != null)
                .GroupBy(l => l.QuestionId)
                .Select(group => new LikeDetails() { QuestionId = (int)group.Key, Count = group.Count() })
                .OrderBy(x => x.QuestionId).ToList();

            var ReputationQuery = from like in db.Likes.ToList()
                                  join question in db.Questions.ToList() on like.QuestionId equals question.Id
                                  select new Reputations()
                                  {
                                      UserId = question.UserId,
                                      IsLiked = like.IsLiked
                                  };

            var ReputationRecords = ReputationQuery.GroupBy(x => new { x.UserId, x.IsLiked }).Select(r => new ReputationsDetails()
            {
                UserId = r.Key.UserId,
                NumberOfReactions = r.Key.IsLiked ? r.Count() : r.Count() * -1
            });

            var ReputationRecordSummarized = ReputationRecords.GroupBy(r => r.UserId).Select(a => new ReputationsDetails()
            {
                UserId = a.Key,
                NumberOfReactions = a.Sum(b => b.NumberOfReactions) * 5
            });

            ViewBag.Reputations = ReputationRecordSummarized;

            bool sorted = false;
            if (sortedByAnswer != null) sorted = (bool)sortedByAnswer;
            if (sorted)
            {
                return View(db.Questions.Include("Answers").Include("Comments").ToList()
                    .OrderByDescending(q => q.Answers.Count()).ToList().ToPagedList(page ?? 1, 10));
            }
            else
            {
                return View(db.Questions.Include("Answers").Include("Comments").ToList().ToPagedList(page ?? 1, 10));
            }
        }

        // GET: Questions
        public ActionResult IndexTags(int? page, int TagId, bool? sortedByAnswer)
        {
            var query = from tag in db.Tags.ToList()
                        join questionTag in db.QuestionTags.ToList() on tag equals questionTag.Tag
                        select new QuestionTagDetails()
                        {
                            QuestionId = questionTag.QuestionId,
                            TagId = questionTag.TagId,
                            Description = tag.Description
                        };

            ViewBag.Tags = query.ToList();
            string UserId = User.Identity.GetUserId();
            ViewBag.Likes = db.Likes.Where(U => U.UserId == UserId).ToList();

            ViewBag.numberOfLikes = db.Likes.Where(z => z.IsLiked == true && z.AnswerId == null)
                .GroupBy(l => l.QuestionId)
                .Select(group => new LikeDetails() { QuestionId = (int)group.Key, Count = group.Count() })
                .OrderBy(x => x.QuestionId).ToList();

            ViewBag.numberOfDisLikes = db.Likes.Where(z => z.IsLiked == false && z.AnswerId == null)
                .GroupBy(l => l.QuestionId)
                .Select(group => new LikeDetails() { QuestionId = (int)group.Key, Count = group.Count() })
                .OrderBy(x => x.QuestionId).ToList();

            ViewBag.numberOfLikesOnAnswer = db.Likes.Where(z => z.IsLiked == true && z.AnswerId != null)
                .GroupBy(l => l.QuestionId)
                .Select(group => new LikeDetails() { QuestionId = (int)group.Key, Count = group.Count() })
                .OrderBy(x => x.QuestionId).ToList();

            ViewBag.numberOfDisLikesOnAnswer = db.Likes.Where(z => z.IsLiked == false && z.AnswerId != null)
                .GroupBy(l => l.QuestionId)
                .Select(group => new LikeDetails() { QuestionId = (int)group.Key, Count = group.Count() })
                .OrderBy(x => x.QuestionId).ToList();

            bool sorted = false;
            if (sortedByAnswer != null) sorted = (bool)sortedByAnswer;

            if (sorted)
            {
                return View(db.Questions.Include("Answers").Include("Comments").ToList()
                    .OrderByDescending(q => q.Answers.Count()).ToList().ToPagedList(page ?? 1, 10));
            }
            else
            {
                return View(db.Questions.Include("Answers").Include("Comments").ToList()
                    .Where(q => db.QuestionTags.ToList()
                    .Where(p2 => p2.TagId == TagId)
                    .Any(qt => qt.QuestionId == q.Id))
                    .OrderByDescending(q => q.Answers.Count()).ToList()
                    .ToPagedList(page ?? 1, 10));
            }
        }

        // GET: Questions/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Tags = new MultiSelectList(db.Tags.ToList().OrderBy(t => t.Description), "Id", "Description");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Title,Description,CreatedAt,Like")] Question question, int[] TagsId)
        {
            question.Id = db.Questions.ToList().Count() + 1;
            question.UserId = User.Identity.GetUserId();
            question.UserName = User.Identity.GetUserName();
            question.CreatedAt = DateTime.Now;

            if (TagsId != null)
            {
                foreach (var tagItemId in TagsId)
                {
                    QuestionTag questionTag = new QuestionTag
                    {
                        Id = db.QuestionTags.ToList().Count() + 1,
                        QuestionId = question.Id,
                        TagId = tagItemId
                    };
                    db.QuestionTags.Add(questionTag);
                }
            }
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                ViewBag.Tags = new MultiSelectList(db.Tags.ToList().OrderBy(t => t.Description), "TagsId", "Description");
                return RedirectToAction("Index");
            }

            return View(question);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Title,Description,CreatedAt,Like")] Question question)
        {
            Question NewQuestion = db.Questions.Find(question.Id);
            NewQuestion.Title = question.Title;
            NewQuestion.Description = question.Description;
            if (ModelState.IsValid)
            {
                db.Entry(NewQuestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(NewQuestion);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);

            foreach (Comment com in db.Comments)
            {
                if (com.QuestionId == question.Id)
                {
                    db.Comments.Remove(com);
                    db.SaveChanges();
                }
            }

            foreach (Like like in db.Likes)
            {
                if (like.QuestionId == question.Id)
                {
                    db.Likes.Remove(like);
                    db.SaveChanges();
                }
            }

            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
