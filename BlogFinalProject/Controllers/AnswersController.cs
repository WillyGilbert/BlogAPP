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
    public class AnswersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Answers/Create
        public ActionResult Create(int QuestionId)
        {
            Answer answer = new Answer();
            answer.QuestionId = QuestionId;
            ViewBag.QuestionId = db.Questions.ToList().Find(a => a.Id == QuestionId);
            return View(answer);
        }

        // POST: Answers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,UserName,QuestionId,Title,Description,CreatedAt,Like,AcceptedAnswer")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                answer.UserId = User.Identity.GetUserId();
                answer.UserName = User.Identity.GetUserName();
                answer.CreatedAt = DateTime.Now;
                db.Answers.Add(answer);
                db.SaveChanges();
                return RedirectToAction("Index", "Questions");
            }

            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "Title", answer.QuestionId);
            return View(answer);
        }
        // GET: Answers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionId = db.Questions.ToList().Find(a => a.Id == answer.QuestionId);
            return View(answer);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,QuestionId,Title,Description,CreatedAt,Like,AcceptedAnswer")] Answer answer)
        {
            Answer NewAnswer = db.Answers.Find(answer.Id);
            NewAnswer.Title = answer.Title;
            NewAnswer.Description = answer.Description;
            if (ModelState.IsValid)
            {
                db.Entry(NewAnswer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Questions");
            }
            ViewBag.QuestionId = db.Questions.ToList().Find(a => a.Id == answer.QuestionId);
            return View(NewAnswer);
        }

        // GET: Answers/AcceptedAnswer/5
        public ActionResult EditAcceptedAnswer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionId = db.Questions.ToList().Find(a => a.Id == answer.QuestionId);
            return View(answer);
        }

        // POST: Answers/AcceptedAnswer/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAcceptedAnswer(int id, bool AcceptedAnswer)
        {
            Answer NewAnswer = db.Answers.Find(id);

            var answers = db.Answers.ToList();

            if (ModelState.IsValid)
            {
                answers.ForEach(a => a.AcceptedAnswer = false);
                NewAnswer.AcceptedAnswer = AcceptedAnswer;
                db.Entry(NewAnswer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Questions");
            }
            ViewBag.QuestionId = db.Questions.ToList().Find(a => a.Id == NewAnswer.QuestionId);
            return View(NewAnswer);
        }

        // GET: Answers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionId = db.Questions.ToList().Find(a => a.Id == answer.QuestionId);
            return View(answer);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Answer answer = db.Answers.Find(id);

            foreach (Comment com in db.Comments)
            {
                if (com.AnswerId == answer.Id)
                {
                    db.Comments.Remove(com);
                }
            }

            foreach (Like like in db.Likes)
            {
                if (like.AnswerId == answer.Id)
                {
                    db.Likes.Remove(like);
                }
            }

            db.Answers.Remove(answer);
            db.SaveChanges();
            ViewBag.QuestionId = db.Questions.ToList().Find(a => a.Id == answer.QuestionId);
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
