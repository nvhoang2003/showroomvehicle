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
    public class VehicleServiceController : Controller
    {
        private showroomEntities db = new showroomEntities();

        // GET: VehicleService
        public ActionResult Index()
        {
            var vehicle_service = db.vehicle_service.Include(v => v.service_order).Include(v => v.vehicle_data);
            return View(vehicle_service.ToList());
        }

        // GET: VehicleService/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vehicle_service vehicle_service = db.vehicle_service.Find(id);
            if (vehicle_service == null)
            {
                return HttpNotFound();
            }
            return View(vehicle_service);
        }

        // GET: VehicleService/Create
        public ActionResult Create()
        {
            ViewBag.service_id = new SelectList(db.service_order, "service_id", "name");
            ViewBag.vehicle_id = new SelectList(db.vehicle_data, "vehicle_data_id", "model_number");
            return View();
        }

        // POST: VehicleService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "vehicle_id,service_id,id_number")] vehicle_service vehicle_service)
        {
            if (ModelState.IsValid)
            {
                db.vehicle_service.Add(vehicle_service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.service_id = new SelectList(db.service_order, "service_id", "name", vehicle_service.service_id);
            ViewBag.vehicle_id = new SelectList(db.vehicle_data, "vehicle_data_id", "model_number", vehicle_service.vehicle_id);
            return View(vehicle_service);
        }

        // GET: VehicleService/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vehicle_service vehicle_service = db.vehicle_service.Find(id);
            if (vehicle_service == null)
            {
                return HttpNotFound();
            }
            ViewBag.service_id = new SelectList(db.service_order, "service_id", "name", vehicle_service.service_id);
            ViewBag.vehicle_id = new SelectList(db.vehicle_data, "vehicle_data_id", "model_number", vehicle_service.vehicle_id);
            return View(vehicle_service);
        }

        // POST: VehicleService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "vehicle_id,service_id,id_number")] vehicle_service vehicle_service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle_service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.service_id = new SelectList(db.service_order, "service_id", "name", vehicle_service.service_id);
            ViewBag.vehicle_id = new SelectList(db.vehicle_data, "vehicle_data_id", "model_number", vehicle_service.vehicle_id);
            return View(vehicle_service);
        }

        // GET: VehicleService/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vehicle_service vehicle_service = db.vehicle_service.Find(id);
            if (vehicle_service == null)
            {
                return HttpNotFound();
            }
            return View(vehicle_service);
        }

        // POST: VehicleService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            vehicle_service vehicle_service = db.vehicle_service.Find(id);
            db.vehicle_service.Remove(vehicle_service);
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
