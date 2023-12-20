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
    public class VehiclePurchaseController : Controller
    {
        private showroomEntities db = new showroomEntities();

        // GET: VehiclePurchase
        public ActionResult Index()
        {
            var vehicle_purchase = db.vehicle_purchase.Include(v => v.purchase_order).Include(v => v.vehicle_data);
            return View(vehicle_purchase.ToList());
        }

        // GET: VehiclePurchase/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vehicle_purchase vehicle_purchase = db.vehicle_purchase.Find(id);
            if (vehicle_purchase == null)
            {
                return HttpNotFound();
            }
            return View(vehicle_purchase);
        }

        // GET: VehiclePurchase/Create
        public ActionResult Create()
        {
            ViewBag.purchase_order_id = new SelectList(db.purchase_order, "purchase_id", "purchase_id");
            ViewBag.vehicle_id = new SelectList(db.vehicle_data, "vehicle_data_id", "model_number");
            return View();
        }

        // POST: VehiclePurchase/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "vehicle_id,purchase_order_id,quantity,price")] vehicle_purchase vehicle_purchase)
        {
            if (ModelState.IsValid)
            {
                db.vehicle_purchase.Add(vehicle_purchase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.purchase_order_id = new SelectList(db.purchase_order, "purchase_id", "purchase_id", vehicle_purchase.purchase_order_id);
            ViewBag.vehicle_id = new SelectList(db.vehicle_data, "vehicle_data_id", "model_number", vehicle_purchase.vehicle_id);
            return View(vehicle_purchase);
        }

        // GET: VehiclePurchase/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vehicle_purchase vehicle_purchase = db.vehicle_purchase.Find(id);
            if (vehicle_purchase == null)
            {
                return HttpNotFound();
            }
            ViewBag.purchase_order_id = new SelectList(db.purchase_order, "purchase_id", "purchase_id", vehicle_purchase.purchase_order_id);
            ViewBag.vehicle_id = new SelectList(db.vehicle_data, "vehicle_data_id", "model_number", vehicle_purchase.vehicle_id);
            return View(vehicle_purchase);
        }

        // POST: VehiclePurchase/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "vehicle_id,purchase_order_id,quantity,price")] vehicle_purchase vehicle_purchase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle_purchase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.purchase_order_id = new SelectList(db.purchase_order, "purchase_id", "purchase_id", vehicle_purchase.purchase_order_id);
            ViewBag.vehicle_id = new SelectList(db.vehicle_data, "vehicle_data_id", "model_number", vehicle_purchase.vehicle_id);
            return View(vehicle_purchase);
        }

        // GET: VehiclePurchase/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vehicle_purchase vehicle_purchase = db.vehicle_purchase.Find(id);
            if (vehicle_purchase == null)
            {
                return HttpNotFound();
            }
            return View(vehicle_purchase);
        }

        // POST: VehiclePurchase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            vehicle_purchase vehicle_purchase = db.vehicle_purchase.Find(id);
            db.vehicle_purchase.Remove(vehicle_purchase);
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
