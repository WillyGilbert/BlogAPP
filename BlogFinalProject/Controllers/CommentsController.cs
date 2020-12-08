using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogFinalProject.Models;
using Microsoft.AspNet.Identity;

namespace BlogFinalProject.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments/Create
        public ActionResult Create(int? QuestionId, int? AnswerId)
        {
            if (AnswerId == 0)
            {
                Comment comment = new Comment();
                comment.QuestionId = QuestionId;
                comment.AnswerId = null;
                comment.CreatedAt = DateTime.Now;
                ViewBag.QuestionId = db.Questions.ToList().Find(a => a.Id == QuestionId);
                ViewBag.AnswerId = db.Answers.ToList().Find(a => a.Id == AnswerId);
                return View(comment);
            }
            else
            {
                Comment comment = new Comment();
                comment.QuestionId = QuestionId;
                comment.AnswerId = AnswerId;
                ViewBag.AnswerId = db.Answers.ToList().Find(a => a.Id == AnswerId);
                ViewBag.QuestionId = db.Questions.ToList().Find(a => a.Id == QuestionId);
                return View(comment);
            }
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,UserName,QuestionId,AnswerId,Description,CreatedAt")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.UserId = User.Identity.GetUserId();
                comment.UserName = User.Identity.GetUserName();
                comment.CreatedAt = DateTime.Now;
                if (comment.AnswerId == 0) comment.AnswerId = null;
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index", "Questions");
            }
            ViewBag.AnswerId = db.Answers.ToList().Find(a => a.Id == comment.AnswerId);
            ViewBag.QuestionId = db.Questions.ToList().Find(a => a.Id == comment.QuestionId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnswerId = db.Answers.ToList().Find(a => a.Id == comment.AnswerId);
            ViewBag.QuestionId = db.Questions.ToList().Find(a => a.Id == comment.QuestionId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,QuestionId,AnswerId,Description,CreatedAt")] Comment comment)
        {
            Comment NewComment = db.Comments.Find(comment.Id);
            NewComment.Description = comment.Description;
            if (ModelState.IsValid)
            {
                db.Entry(NewComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Questions");
            }
            ViewBag.AnswerId = db.Answers.ToList().Find(a => a.Id == comment.AnswerId);
            ViewBag.QuestionId = db.Questions.ToList().Find(a => a.Id == comment.QuestionId);
            return View(NewComment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnswerId = db.Answers.ToList().Find(a => a.Id == comment.AnswerId);
            ViewBag.QuestionId = db.Questions.ToList().Find(a => a.Id == comment.QuestionId);
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            ViewBag.AnswerId = db.Answers.ToList().Find(a => a.Id == comment.AnswerId);
            ViewBag.QuestionId = db.Questions.ToList().Find(a => a.Id == comment.QuestionId);
            return RedirectToAction("Index", "Questions");
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
