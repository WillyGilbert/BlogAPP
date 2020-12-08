using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogFinalProject.Models;

namespace BlogFinalProject.Controllers
{
    public class QuestionTagsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: QuestionTags
        public ActionResult Index()
        {
            var questionTags = db.QuestionTags.Include(q => q.Question).Include(q => q.Tag);
            return View(questionTags.ToList());
        }

        // GET: QuestionTags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionTag questionTag = db.QuestionTags.Find(id);
            if (questionTag == null)
            {
                return HttpNotFound();
            }
            return View(questionTag);
        }

        // GET: QuestionTags/Create
        public ActionResult Create()
        {
            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "Title");
            ViewBag.TagId = new SelectList(db.Tags, "Id", "Description");
            return View();
        }

        // POST: QuestionTags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,QuestionId,TagId")] QuestionTag questionTag)
        {
            if (ModelState.IsValid)
            {
                db.QuestionTags.Add(questionTag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "Title", questionTag.QuestionId);
            ViewBag.TagId = new SelectList(db.Tags, "Id", "Description", questionTag.TagId);
            return View(questionTag);
        }

        // GET: QuestionTags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionTag questionTag = db.QuestionTags.Find(id);
            if (questionTag == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "Title", questionTag.QuestionId);
            ViewBag.TagId = new SelectList(db.Tags, "Id", "Description", questionTag.TagId);
            return View(questionTag);
        }

        // POST: QuestionTags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,QuestionId,TagId")] QuestionTag questionTag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionTag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "Title", questionTag.QuestionId);
            ViewBag.TagId = new SelectList(db.Tags, "Id", "Description", questionTag.TagId);
            return View(questionTag);
        }

        // GET: QuestionTags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionTag questionTag = db.QuestionTags.Find(id);
            if (questionTag == null)
            {
                return HttpNotFound();
            }
            return View(questionTag);
        }

        // POST: QuestionTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionTag questionTag = db.QuestionTags.Find(id);
            db.QuestionTags.Remove(questionTag);
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
