using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogFinalProject.Models;
using Microsoft.AspNet.Identity;

namespace BlogFinalProject.Controllers
{
    public class LikesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Likes/Create
        public ActionResult Create(int? QuestionId, int? AnswerId)
        {
            if (AnswerId == 0)
            {
                Like like = new Like();
                like.QuestionId = QuestionId;
                like.AnswerId = null;
                ViewBag.QuestionId = db.Questions.ToList().Find(a => a.Id == QuestionId);
                ViewBag.AnswerId = db.Answers.ToList().Find(a => a.Id == AnswerId);
                return View(like);
            }
            else
            {
                Like like = new Like();
                like.QuestionId = QuestionId;
                like.AnswerId = AnswerId;
                ViewBag.AnswerId = db.Answers.ToList().Find(a => a.Id == AnswerId);
                ViewBag.QuestionId = db.Questions.ToList().Find(a => a.Id == QuestionId);
                return View(like);
            }
        }

        // POST: Likes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,UserName,QuestionId,AnswerId,IsLiked")] Like like)
        {
            if (ModelState.IsValid)
            {
                like.UserId = User.Identity.GetUserId();
                like.UserName = User.Identity.GetUserName();
                if (like.AnswerId == 0) like.AnswerId = null;
                db.Likes.Add(like);
                db.SaveChanges();
                return RedirectToAction("Index", "Questions");
            }
            ViewBag.AnswerId = db.Answers.ToList().Find(a => a.Id == like.AnswerId);
            ViewBag.QuestionId = db.Questions.ToList().Find(a => a.Id == like.QuestionId);
            return View(like);
        }

        // GET: Likes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Like like = db.Likes.Find(id);
            if (like == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnswerId = db.Answers.ToList().Find(a => a.Id == like.AnswerId);
            ViewBag.QuestionId = db.Questions.ToList().Find(a => a.Id == like.QuestionId);
            return View(like);
        }

        // POST: Likes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,UserId,UserName,QuestionId,AnswerId,IsLiked")] Like like)
        public ActionResult Edit(int Id, bool IsLiked)
        {
            Like NewLike = db.Likes.Find(Id);
            NewLike.IsLiked = IsLiked;

            if (ModelState.IsValid)
            {
                db.Entry(NewLike).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Questions");
            }
            ViewBag.AnswerId = db.Answers.ToList().Find(a => a.Id == NewLike.AnswerId);
            ViewBag.QuestionId = db.Questions.ToList().Find(a => a.Id == NewLike.QuestionId);
            return View(NewLike);
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
