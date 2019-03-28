using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CentricProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace CentricProject.Controllers
{
    
    public class ProfileDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Variable to get the logged in user
        

        // GET: ProfileDetails
        public ActionResult Index(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var testUsers = from u in db.ProfileDetails select u;
                testUsers = testUsers.Where(u => u.lastName.Contains(searchString) || u.firstName.Contains(searchString));
                return View(testUsers.ToList());
            }


            return View(db.ProfileDetails.ToList());
        }
        [Authorize] // Only the logged in user can view thier details
        // GET: ProfileDetails/Details/5
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileDetails profileDetails = db.ProfileDetails.Find(id);
            if (profileDetails == null)
            {
                return HttpNotFound();
            }
            return View(profileDetails);
        }
       
        // GET: ProfileDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProfileDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")] // Profile is created during the registration portion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,firstName,lastName,prefferedName,phoneNumber,hireDate,businessUnit,position")] ProfileDetails profileDetails)
        {
            if (ModelState.IsValid)
            {
                db.ProfileDetails.Add(profileDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(profileDetails);
        }

        // GET: ProfileDetails/Edit/5
        [Authorize] // Only the logged in user can edit thier details
        public ActionResult Edit(int? id)
        {
            // Find out the logged in userId and verify the http request
            if (id != AuthorizeLoggedInUser())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileDetails profileDetails = db.ProfileDetails.Find(id);
            if (profileDetails == null)
            {
                return HttpNotFound();
            }
            return View(profileDetails);
        }

        // POST: ProfileDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize] // Only the logged in user can edit thier details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,firstName,lastName,prefferedName,phoneNumber,hireDate,businessUnit,position", Exclude = "profileImage")] ProfileDetails profileDetails)
        {
            
            if (ModelState.IsValid)
            {
                
                db.Entry(profileDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profileDetails);
        }

        // GET: ProfileDetails/Delete/5
        [Authorize(Roles = "Admin")] // Only Admins can delete the data
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileDetails profileDetails = db.ProfileDetails.Find(id);
            if (profileDetails == null)
            {
                return HttpNotFound();
            }
            return View(profileDetails);
        }

        // POST: ProfileDetails/Delete/5
        [Authorize(Roles = "Admin")] // Only Admins can delete the data
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProfileDetails profileDetails = db.ProfileDetails.Find(id);
            db.ProfileDetails.Remove(profileDetails);
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

        private int AuthorizeLoggedInUser()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            int userId = currentUser.ProfileDetails.id;
            return userId;
        }

        public FileContentResult ProfileImage()
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");
                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);
                    return File(imageData, "image/png");
                }
                var bdUsers = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var userImage = bdUsers.Users.Where(x => x.Id == userId).FirstOrDefault();
                return new FileContentResult(userImage.ProfileDetails.profileImage, "image/png");
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");
                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");
            }
        }
        


    }
}
