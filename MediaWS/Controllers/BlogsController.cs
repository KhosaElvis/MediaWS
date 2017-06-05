using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MediaWS.Models;
using System.IO;

namespace MediaWS.Controllers
{
    public class BlogsController : Controller
    {
        private MediaContent db = new MediaContent();

        // GET: Blogs
        public ActionResult Index()
        {
            return View(db.blogs.ToList());
        }

        // GET: Blogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Blogs/Create
        //public ActionResult Create()
        //{
        //    return View();
        ////}

        //// POST: Blogs/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        
        ////public ActionResult Create(Blog blog, HttpPostedFileBase picture)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //using (var binaryReader = new BinaryReader(picture.InputStream))
        //        //{
        //        //    blog.picture = (binaryReader.ReadBytes(picture.ContentLength));

        //        //}
               

        //        if (picture!=null)
        //        {
        //            //model.ViewImages = new byte[ADDImage.ContentLength];
        //            //ADDImage.InputStream.Read(model.ViewImages, 0, ADDImage.ContentLength);
        //            blog.picture = new byte[picture.ContentLength];
        //            picture.InputStream.Read(blog.picture, 0, picture.ContentLength);

        //        }
        //        db.blogs.Add(blog);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(blog);
        //}

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Blog blg, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    blg.picture = new byte[upload.ContentLength];
                    upload.InputStream.Read(blg.picture, 0, upload.ContentLength);

                    db.blogs.Add(blg);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Blogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,body,picture")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.blogs.Find(id);
            db.blogs.Remove(blog);
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
