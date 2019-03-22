using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CentricProject.Models;

namespace CentricProject.Controllers
{
    public class ProfileDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles ="Admin")] // This is temporary until search function is up to speed
        // GET: ProfileDetails
        public ActionResult Index()
        {
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
        [Authorize(Roles = "Admin")] // Profile is created during the registration portion
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
        public ActionResult Edit([Bind(Include = "id,firstName,lastName,prefferedName,phoneNumber,hireDate,businessUnit,position")] ProfileDetails profileDetails)
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
    }
}
