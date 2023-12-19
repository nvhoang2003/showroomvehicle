using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShowroomManagement.Models;

namespace ShowroomManagement.Controllers
{
    public class VehicleImageController : Controller
    {
        private showroomEntities db = new showroomEntities();

        // GET: VehicleImage
        public ActionResult Index()
        {
            var vehicle_image = db.vehicle_image.Include(v => v.vehicle_data);
            return View(vehicle_image.ToList());
        }

        // GET: VehicleImage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vehicle_image vehicle_image = db.vehicle_image.Find(id);
            if (vehicle_image == null)
            {
                return HttpNotFound();
            }
            return View(vehicle_image);
        }

        // GET: VehicleImage/Create
        public ActionResult Create()
        {
            ViewBag.vehicle_id = new SelectList(db.vehicle_data, "vehicle_data_id", "model_number");
            return View();
        }

        // POST: VehicleImage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "image_id,vehicle_id,image_url")] vehicle_image vehicle_image)
        {
            if (ModelState.IsValid)
            {
                db.vehicle_image.Add(vehicle_image);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.vehicle_id = new SelectList(db.vehicle_data, "vehicle_data_id", "model_number", vehicle_image.vehicle_id);
            return View(vehicle_image);
        }

        // GET: VehicleImage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vehicle_image vehicle_image = db.vehicle_image.Find(id);
            if (vehicle_image == null)
            {
                return HttpNotFound();
            }
            ViewBag.vehicle_id = new SelectList(db.vehicle_data, "vehicle_data_id", "model_number", vehicle_image.vehicle_id);
            return View(vehicle_image);
        }

        // POST: VehicleImage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "image_id,vehicle_id,image_url")] vehicle_image vehicle_image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle_image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.vehicle_id = new SelectList(db.vehicle_data, "vehicle_data_id", "model_number", vehicle_image.vehicle_id);
            return View(vehicle_image);
        }

        // GET: VehicleImage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vehicle_image vehicle_image = db.vehicle_image.Find(id);
            if (vehicle_image == null)
            {
                return HttpNotFound();
            }
            return View(vehicle_image);
        }

        // POST: VehicleImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            vehicle_image vehicle_image = db.vehicle_image.Find(id);
            db.vehicle_image.Remove(vehicle_image);
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
