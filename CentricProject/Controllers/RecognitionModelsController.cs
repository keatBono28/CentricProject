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
    public class RecognitionModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RecognitionModels
        public ActionResult Index()
        {
            var recognitionModels = db.RecognitionModels.Include(r => r.Recognized);
            return View(recognitionModels.ToList());
        }

        // GET: RecognitionModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecognitionModel recognitionModel = db.RecognitionModels.Find(id);
            if (recognitionModel == null)
            {
                return HttpNotFound();
            }
            return View(recognitionModel);
        }

        // GET: RecognitionModels/Create
        public ActionResult Create()
        {
            ViewBag.recognizedId = new SelectList(db.ProfileDetails, "id", "firstName");
            ViewBag.recognizerId = new SelectList(db.ProfileDetails, "id", "firstName");
            return View();
        }

        // POST: RecognitionModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "recognitionId,recognizerId,recognizedId,coreValue,comments,createDate")] RecognitionModel recognitionModel)
        {
            if (ModelState.IsValid)
            {
                db.RecognitionModels.Add(recognitionModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.recognizedId = new SelectList(db.ProfileDetails, "id", "firstName", recognitionModel.recognizedId);
            ViewBag.recognizerId = new SelectList(db.ProfileDetails, "id", "firstName", recognitionModel.recognizerId);
            return View(recognitionModel);
        }

        // GET: RecognitionModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecognitionModel recognitionModel = db.RecognitionModels.Find(id);
            if (recognitionModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.recognizedId = new SelectList(db.ProfileDetails, "id", "firstName", recognitionModel.recognizedId);
            ViewBag.recognizerId = new SelectList(db.ProfileDetails, "id", "firstName", recognitionModel.recognizerId);
            return View(recognitionModel);
        }

        // POST: RecognitionModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "recognitionId,recognizerId,recognizedId,coreValue,comments,createDate")] RecognitionModel recognitionModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recognitionModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.recognizedId = new SelectList(db.ProfileDetails, "id", "firstName", recognitionModel.recognizedId);
            ViewBag.recognizerId = new SelectList(db.ProfileDetails, "id", "firstName", recognitionModel.recognizerId);
            return View(recognitionModel);
        }

        // GET: RecognitionModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecognitionModel recognitionModel = db.RecognitionModels.Find(id);
            if (recognitionModel == null)
            {
                return HttpNotFound();
            }
            return View(recognitionModel);
        }

        // POST: RecognitionModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RecognitionModel recognitionModel = db.RecognitionModels.Find(id);
            db.RecognitionModels.Remove(recognitionModel);
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
