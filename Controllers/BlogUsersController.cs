using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShauliFinalProject.Models;
using ShauliFinalProject.Tools;

namespace ShauliFinalProject.Controllers
{
    public class BlogUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BlogUsers
        public ActionResult Index()
        {
            return View(db.BlogUsers.ToList());
        }

        // GET: BlogUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogUser blogUser = db.BlogUsers.Find(id);
            if (blogUser == null)
            {
                return HttpNotFound();
            }
            return View(blogUser);
        }

        // GET: BlogUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Password,IsAdmin")] BlogUser blogUser)
        {
            if (ModelState.IsValid)
            {
                db.BlogUsers.Add(blogUser);
                db.SaveChanges();
                LoginManager.Instance.CurrentLogedUser = blogUser;
                return RedirectToAction("Index", "Blog");
            }

            return View(blogUser);
        }

        // GET: BlogUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogUser blogUser = db.BlogUsers.Find(id);
            if (blogUser == null)
            {
                return HttpNotFound();
            }
            return View(blogUser);
        }

        // POST: BlogUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Password,IsAdmin")] BlogUser blogUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogUser);
        }

        // GET: BlogUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogUser blogUser = db.BlogUsers.Find(id);
            if (blogUser == null)
            {
                return HttpNotFound();
            }
            return View(blogUser);
        }

        // POST: BlogUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogUser blogUser = db.BlogUsers.Find(id);
            db.BlogUsers.Remove(blogUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Users/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Username,Password")] BlogUser fan)
        {
            if (ModelState.IsValid)
            {
                BlogUser loggedUser = db.BlogUsers.Where(user => user.UserName.Equals(fan.UserName) && user.Password.Equals(fan.Password)).FirstOrDefault();
                if (loggedUser != null)
                {
                    LoginManager.Instance.CurrentLogedUser = loggedUser;

                    return RedirectToAction("Index", "Blog");
                }
                ViewBag.ErrorMessage = "Invalid Username or Password! Please try again..";
                return View();
            }

            return RedirectToAction("Error");
        }


        // GET: Users/Login
        public ActionResult Logout()
        {
            LoginManager.Instance.CurrentLogedUser = null;
            return RedirectToAction("Index", "Blog");
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
