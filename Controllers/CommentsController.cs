using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShauliFinalProject.Models;
using System.Globalization;

namespace ShauliFinalProject.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        public ActionResult Index(int? id)
        {
            var post = db.Posts.Where(p => p.Id == id).Include(p => p.Comments);

            if(post.FirstOrDefault() != null)
                return View(post.FirstOrDefault().Comments);
            return View(db.Comments);
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
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
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.PostId = new SelectList(db.Posts, "Id", "Headline");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PostId,Headline,Author,Website,Timestamp,Content,Post")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.Timestamp = DateTime.Now;
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index", "Blog");
            }

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
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PostId,Headline,Author,Website,Timestamp,Content,Post")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", comment.PostId);
            }
            return View(comment);
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
            return RedirectToAction("Index");
        }

        public ActionResult Search(int? id, string author, string text, string postedAfter, string postedBefore)
        {
            List<Comment> results = db.Comments.ToList();
            DateTime date;

            // Checks whether to filter by id
            if (id.HasValue)
            {
                results = results.Where(comment => comment.Id == id.Value).ToList();
            }

            // Checks whether to filter by author
            if (!string.IsNullOrWhiteSpace(author))
            {
                results = results.Where(comment => comment.Author.Contains(author)).ToList();
            }

            // Checks whether to filter by some sort of text
            if (!string.IsNullOrWhiteSpace(text))
            {
                results = results.Where(comment => comment.Headline.Contains(text) || comment.Content.Contains(text)).ToList();
            }

            // Checks whether to get comments posted after a certain date
            if (postedAfter != null && DateTime.TryParseExact(postedAfter, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                results = results.Where(comment => comment.Timestamp > date).ToList();
            }

            // Checks whether to get comments posted before a certain date
            if (postedBefore != null && DateTime.TryParseExact(postedBefore, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                results = results.Where(comment => comment.Timestamp < date).ToList();
            }

            return View("Result", results);
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
