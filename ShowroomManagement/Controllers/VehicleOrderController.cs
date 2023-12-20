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
    public class VehicleOrderController : Controller
    {
        private showroomEntities db = new showroomEntities();

        // GET: VehicleOrder
        public ActionResult Index()
        {
            var vehicle_order = db.vehicle_order.Include(v => v.order).Include(v => v.vehicle_data);
            return View(vehicle_order.ToList());
        }

        // GET: VehicleOrder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vehicle_order vehicle_order = db.vehicle_order.Find(id);
            if (vehicle_order == null)
            {
                return HttpNotFound();
            }
            return View(vehicle_order);
        }

        // GET: VehicleOrder/Create
        public ActionResult Create()
        {
            ViewBag.order_id = new SelectList(db.orders, "order_id", "status");
            ViewBag.vehicle_id = new SelectList(db.vehicle_data, "vehicle_data_id", "model_number");
            return View();
        }

        // POST: VehicleOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "vehicle_id,order_id,id_number")] vehicle_order vehicle_order)
        {
            if (ModelState.IsValid)
            {
                db.vehicle_order.Add(vehicle_order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.order_id = new SelectList(db.orders, "order_id", "status", vehicle_order.order_id);
            ViewBag.vehicle_id = new SelectList(db.vehicle_data, "vehicle_data_id", "model_number", vehicle_order.vehicle_id);
            return View(vehicle_order);
        }

        // GET: VehicleOrder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vehicle_order vehicle_order = db.vehicle_order.Find(id);
            if (vehicle_order == null)
            {
                return HttpNotFound();
            }
            ViewBag.order_id = new SelectList(db.orders, "order_id", "status", vehicle_order.order_id);
            ViewBag.vehicle_id = new SelectList(db.vehicle_data, "vehicle_data_id", "model_number", vehicle_order.vehicle_id);
            return View(vehicle_order);
        }

        // POST: VehicleOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "vehicle_id,order_id,id_number")] vehicle_order vehicle_order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle_order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.order_id = new SelectList(db.orders, "order_id", "status", vehicle_order.order_id);
            ViewBag.vehicle_id = new SelectList(db.vehicle_data, "vehicle_data_id", "model_number", vehicle_order.vehicle_id);
            return View(vehicle_order);
        }

        // GET: VehicleOrder/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vehicle_order vehicle_order = db.vehicle_order.Find(id);
            if (vehicle_order == null)
            {
                return HttpNotFound();
            }
            return View(vehicle_order);
        }

        // POST: VehicleOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            vehicle_order vehicle_order = db.vehicle_order.Find(id);
            db.vehicle_order.Remove(vehicle_order);
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
