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
    public class FansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Fans
        public ActionResult Index()
        {
            return View(db.Fans.ToList());
        }

        // GET: Fans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // GET: Fans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Type,DateOfBirth,Seniority")] Fan fan)
        {
            if (ModelState.IsValid)
            {
                db.Fans.Add(fan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fan);
        }

        // GET: Fans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // POST: Fans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Type,DateOfBirth,Seniority")] Fan fan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fan);
        }

        // GET: Fans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // POST: Fans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fan fan = db.Fans.Find(id);
            db.Fans.Remove(fan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Search(int? id, string firstName, string lastName, int? seniority, string bornBefore, string bornAfter)
        {
            List<Fan> results = db.Fans.ToList();
            DateTime date;

            // Checks whether to filter by id
            if (id.HasValue)
            {
                results = results.Where(fan => fan.ID == id.Value).ToList();
            }

            // Checks whether to filter by firstname
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                results = results.Where(fan => fan.FirstName == firstName).ToList();
            }

            // Checks whether to filter by lastname
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                results = results.Where(fan => fan.LastName == lastName).ToList();
            }

            // Checks whether to filter by seniority
            if (seniority.HasValue)
            {
                   results = results.Where(fan => fan.Seniority == seniority.Value).ToList();
            }

            if (bornAfter != null && DateTime.TryParseExact(bornAfter, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                results = results.Where(fan => fan.DateOfBirth > date).ToList();
            }

            if (bornBefore != null && DateTime.TryParseExact(bornBefore, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                results = results.Where(fan => fan.DateOfBirth < date).ToList();
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
