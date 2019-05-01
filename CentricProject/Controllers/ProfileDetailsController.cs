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
            if (id != AuthorizeLoggedInUser())
            {
                // User is attempting access to another account. 
                // Send them to error page
                return RedirectToAction("Index", "Error");

            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileDetails profileDetails = db.ProfileDetails.Find(id);
            RecognitionModel recognitionModels = db.RecognitionModels.Find(id);
            if (profileDetails == null)
            {
                return RedirectToAction("Index", "Error");
                //return HttpNotFound();
            }
            return View(Tuple.Create(profileDetails,recognitionModels));
        }
       
        // GET: ProfileDetails/Create
        public ActionResult Create()
        {
            // The only time an account can be created is during registration.
            // This functionality will be disabled
            // Return the custom error page
            return RedirectToAction("Index", "Error");
            //return View();
        }

        // POST: ProfileDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")] // Profile is created during the registration portion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,firstName,lastName,prefferedName,phoneNumber,hireDate,businessUnit,position")] ProfileDetails profileDetails)
        {

            // The only time an account can be created is during registration.
            // This functionality will be disabled
            // Return the custom error page
            return RedirectToAction("Index", "Error");
            /*if (ModelState.IsValid)
            {
                db.ProfileDetails.Add(profileDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(profileDetails);*/
        }

        // GET: ProfileDetails/Edit/5
        [Authorize] // Only the logged in user can edit thier details
        public ActionResult Edit(int? id)
        {
            // Find out the logged in userId and verify the http request
            if (id != AuthorizeLoggedInUser())
            {
                // User is attempting access to another account. 
                // Send them to error page
                return RedirectToAction("Index", "Error");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
        public ActionResult Edit([Bind(Exclude = "profileImage")]ProfileDetails profileDetails, bool useOldImage = false)
        {
            int userId = AuthorizeLoggedInUser();
            byte[] imageData = null;
            if (useOldImage == false)
            {
                // Convert the user upload to byte array
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase poImageFile = Request.Files["profileImageUpdate"];
                    using (var binary = new BinaryReader(poImageFile.InputStream))
                    {
                        imageData = binary.ReadBytes(poImageFile.ContentLength);
                    }
                }
            }
            // Retrieving old profile details to get old image
            ProfileDetails oldProfile = db.ProfileDetails.Find(userId);
            if (ModelState.IsValid)
            {
                var newInfo = db.ProfileDetails.Find(AuthorizeLoggedInUser());
                
                newInfo.firstName = profileDetails.firstName;
                newInfo.lastName = profileDetails.lastName;
                newInfo.prefferedName = profileDetails.prefferedName;
                newInfo.phoneNumber = profileDetails.phoneNumber;
                newInfo.hireDate = profileDetails.hireDate;
                newInfo.businessUnit = profileDetails.businessUnit;
                newInfo.position = profileDetails.position;
                if (useOldImage == true)
                {
                    newInfo.profileImage = oldProfile.profileImage;
                }
                else
                {
                    // Double checking user input. 
                    if (imageData != null)
                    {
                        newInfo.profileImage = imageData;
                    }
                    else
                    {
                        // If we get here, the user unchecked the box, but
                        // did not upload new image. We will default to old image
                        newInfo.profileImage = oldProfile.profileImage;
                    }
                }
                db.SaveChanges();
                
                return RedirectToAction("Details", new { id = profileDetails.id });
            }
            return View(profileDetails);
        }

        // GET: ProfileDetails/Delete/5
        [Authorize(Roles = "Admin")] // Only Admins can delete the data
        public ActionResult Delete(int? id)
        {
            // User is attempting to delete an account. 
            // Send them to error page
            return RedirectToAction("Index", "Error");

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //ProfileDetails profileDetails = db.ProfileDetails.Find(id);
            //if (profileDetails == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(profileDetails);
        }

        // POST: ProfileDetails/Delete/5
        [Authorize(Roles = "Admin")] // Only Admins can delete the data
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // User is attempting to delete an account. 
            // Send them to error page
            return RedirectToAction("Index", "Error");
            //ProfileDetails profileDetails = db.ProfileDetails.Find(id);
            //db.ProfileDetails.Remove(profileDetails);
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
            int userId = 0;
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            if (manager.FindById(User.Identity.GetUserId()) != null)
            {
                var currentUser = manager.FindById(User.Identity.GetUserId());
                userId = currentUser.ProfileDetails.id;
                return userId;
            }
            else
            {
                return userId;
            }
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
