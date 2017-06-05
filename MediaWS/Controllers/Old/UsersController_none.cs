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
    public class UsersController : Controller
    {
        private MediaContent db = new MediaContent();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,surname,email,password,confirm,idOfbirth")] User user)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,surname,email,password,confirm,idOfbirth")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.users.Find(id);
            db.users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // Login Customised 
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            
            var user = db.users.FirstOrDefault(m => m.email == email);
            try
            {
                bool isCorrect = user.password == password;
                if (isCorrect)
                {
                    //Check if the user's email address is verified 
                    int user_id = user.id;
                    var verify = db.verifies.FirstOrDefault(m => m.id == user_id);
                    bool isValid = verify.isVerified;
                    if (isValid)
                    {
                        // Create the user session
                        Session["Lg_id"] = user.id.ToString();
                        Session["Lg_name"] = user.name.ToString();
                        Session["Lg_lname"] = user.surname.ToString();
                        Session["Lg_email"] = user.email.ToString();
                        //Session["Lg_picture"] = user.pi                        //redirect user to wall
                        return RedirectToAction("Index", "Blogs");
                    }
                    else
                    {
                        //Tell the user to verify email
                        return Redirect("http://www.yahoo.com");
                    }
                }
                else
                {
                    // Return incorect password, and increment wrong password entered
                    // Check if wrong password was entered 3 times and if true send the email with link to reset password
                    return Redirect("http://www.google.com");
                }

            }
            catch (NullReferenceException)
            {
                return Redirect("http://www.facebook.com");
            }
            
            
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
