using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CentricProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net.Mail;


namespace CentricProject.Controllers
{
    public class RecognitionModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RecognitionModels
        public ActionResult Index()
        {
            var recognitionModels = db.RecognitionModels.Include(r => r.Recognizer).OrderByDescending(r => r.createDate);
            return View(recognitionModels.ToList());
        }

        // GET: RecognitionModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Error");
            }
            RecognitionModel recognitionModel = db.RecognitionModels.Find(id);
            ProfileDetails profileDetails = db.ProfileDetails.Find(id);
            if (recognitionModel == null)
            {
                return RedirectToAction("Index", "Error");
            }
            return View(Tuple.Create(recognitionModel,profileDetails));
        }

        // GET: RecognitionModels/Create/?id
        public ActionResult Create(int? id)
        {
            ViewBag.recognizedId = id;
            return View();
        }

        // POST: RecognitionModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "recognitionId,recognizerId,recognizedId,coreValue,comments,createDate")] RecognitionModel recognitionModel, string recognizedId)
        {
            if (Convert.ToInt32(recognizedId) == AuthorizeLoggedInUser())
            {
                // Throw to the error page, user cannot create for themselves
                return RedirectToAction("Index", "Error");
            }
            string email = TempData["email"].ToString();
            if (ModelState.IsValid)
            {
                recognitionModel.recognizerId = AuthorizeLoggedInUser();
                recognitionModel.recognizedId = Convert.ToInt32(recognizedId);
                recognitionModel.createDate = DateTime.Now;
                db.RecognitionModels.Add(recognitionModel);
                db.SaveChanges();
                // send the email to notify
                try
                {
                    SendEmailAlert(Convert.ToInt32(recognizedId), recognitionModel.recognizerId, email);
                }
                catch (Exception)
                {
                    
                    //throw;
                }
                
                return Redirect("~/ProfileDetails/Index");
            }

            ViewBag.recognizedId = new SelectList(db.ProfileDetails, "id", "firstName", recognitionModel.recognizedId);
            ViewBag.recognizerId = new SelectList(db.ProfileDetails, "id", "firstName", recognitionModel.recognizerId);
            return View(recognitionModel);
        }

        // GET: RecognitionModels/Edit/5
        public ActionResult Edit(int? id)
        {
            // Users should not be allowed to edit a recognition
            return RedirectToAction("Index", "Error");
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //RecognitionModel recognitionModel = db.RecognitionModels.Find(id);
            //if (recognitionModel == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.recognizedId = new SelectList(db.ProfileDetails, "id", "firstName", recognitionModel.recognizedId);
            //ViewBag.recognizerId = new SelectList(db.ProfileDetails, "id", "firstName", recognitionModel.recognizerId);
            //return View(recognitionModel);
        }

        // POST: RecognitionModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "recognitionId,recognizerId,recognizedId,coreValue,comments,createDate")] RecognitionModel recognitionModel)
        {
            // Users are not allowed to edit a recognition
            return RedirectToAction("Index", "Error");
            //if (ModelState.IsValid)
            //{
            //    db.Entry(recognitionModel).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.recognizedId = new SelectList(db.ProfileDetails, "id", "firstName", recognitionModel.recognizedId);
            //ViewBag.recognizerId = new SelectList(db.ProfileDetails, "id", "firstName", recognitionModel.recognizerId);
            //return View(recognitionModel);
        }

        // GET: RecognitionModels/Delete/5
        public ActionResult Delete(int? id)
        {
            // Users are not allowed to delete a recognition
            return RedirectToAction("Index", "Error");
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //RecognitionModel recognitionModel = db.RecognitionModels.Find(id);
            //if (recognitionModel == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(recognitionModel);
        }

        // POST: RecognitionModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Users are not allowed to delete a recognition
            return RedirectToAction("Index", "Error");
            //RecognitionModel recognitionModel = db.RecognitionModels.Find(id);
            //db.RecognitionModels.Remove(recognitionModel);
            //db.SaveChanges();
            //return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private int AuthorizeLoggedInUser()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            int userId = currentUser.ProfileDetails.id;
            return userId;
        }


        public void SendEmailAlert(int recognizedId, int recognizerId, string email)
        {
            
            var recognizer = db.ProfileDetails.Find(recognizerId);
            var recognized = db.ProfileDetails.Find(recognizedId);
            //var recognizedUser = db.Users.Find();
            // This method will send an email alert to the user if they are recognized.
            SmtpClient myClient = new SmtpClient();
            myClient.Credentials = new NetworkCredential("kb619814@ohio.edu", "Thecrew28");
            // Build the mail address 
            MailAddress from = new MailAddress("kb619814@ohio.edu", "System Admin");
            // Build email message to send
            MailMessage newMessage = new MailMessage();
            newMessage.From = from;
            newMessage.To.Add(email);
            newMessage.Subject = "Wow great job! You have been recognized by " + recognizer.prefferedName.ToString();
            newMessage.Body = "Wow you did great work. You have been recognized by a coworker for something awesome.";
            newMessage.Body += "Please login to see what they said!";
            //Now try to send the message
            try
            {
                myClient.Send(newMessage);
                TempData["mailError"] = "";
            }
            catch (Exception ex)
            {
                // Do nothing it didn't work.
                // Email confirmation is not set up
                TempData["mailError"] = ex.ToString();
            }





        }


        
    }
}
