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
using System.Net.Mail;

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
                // Send Verification Email
                try
                {
                    string email = user.email;
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.To.Add(email);
                    mailMessage.From = new MailAddress("maelvious@gmail.com");
                    mailMessage.Subject = "ASP.NET e-mail test";
                    mailMessage.Body = "Hello world,\n\nThis is an ASP.NET test e-mail!";
                    SmtpClient smtpClient = new SmtpClient("smtp.your-isp.com");
                    smtpClient.Send(mailMessage);
                    Response.Write("E-mail sent!");
                }
                catch (Exception ex)
                {
                    Response.Write("Could not send the e-mail - error: " + ex.Message);
                }
                db.SaveChanges();
                //Create Folder to associate with the user
                string root = @"C:\Users\Smartwereld\Documents\Visual Studio 2017\Projects\MediaWS\Images";
                string uprofile = @"C:\Users\Smartwereld\Documents\Visual Studio 2017\Projects\MediaWS\Images/" + user.email.ToString();
                if(!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }

                if (!Directory.Exists(uprofile))
                {
                    Directory.CreateDirectory(uprofile);
                    
                }

                
               
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

        public ActionResult Logout()
        {
            Session["Lg_id"] = null;
            Session["Lg_name"] = null;
            Session["Lg_lname"] = null;
            Session["Lg_email"] = null;
            return RedirectToAction("Index", "Blogs");
        } 
        //Lougt

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
                    Verify verify = db.verifies.FirstOrDefault(m => m.user_id == user_id);
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
                // Email Not Found
                return Redirect("http://www.facebook.com");
            }

        }

        // Logout
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
