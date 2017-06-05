using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MediaWS.Models;

namespace MediaWS.Controllers
{
    public class VerifiesController : Controller
    {
        private MediaContent db = new MediaContent();

        // GET: Verifies
        public ActionResult Index()
        {
            return View(db.verifies.ToList());
        }

        // GET: Verifies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verify verify = db.verifies.Find(id);
            if (verify == null)
            {
                return HttpNotFound();
            }
            return View(verify);
        }

        // GET: Verifies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Verifies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,isVerified")] Verify verify)
        {
            if (ModelState.IsValid)
            {
                db.verifies.Add(verify);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(verify);
        }

        // GET: Verifies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verify verify = db.verifies.Find(id);
            if (verify == null)
            {
                return HttpNotFound();
            }
            return View(verify);
        }

        // POST: Verifies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,isVerified")] Verify verify)
        {
            if (ModelState.IsValid)
            {
                db.Entry(verify).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(verify);
        }

        // GET: Verifies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verify verify = db.verifies.Find(id);
            if (verify == null)
            {
                return HttpNotFound();
            }
            return View(verify);
        }

        // POST: Verifies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Verify verify = db.verifies.Find(id);
            db.verifies.Remove(verify);
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
