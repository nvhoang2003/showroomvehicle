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
    public class PreOrderController : Controller
    {
        private showroomEntities db = new showroomEntities();

        // GET: PreOrder
        public ActionResult Index()
        {
            var pre_order = db.pre_order.Include(p => p.customer).Include(p => p.vehicle_data);
            return View(pre_order.ToList());
        }

        // GET: PreOrder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pre_order pre_order = db.pre_order.Find(id);
            if (pre_order == null)
            {
                return HttpNotFound();
            }
            return View(pre_order);
        }

        // GET: PreOrder/Create
        public ActionResult Create()
        {
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "user_name");
            ViewBag.vehicle_id = new SelectList(db.vehicle_data, "vehicle_data_id", "model_number");
            return View();
        }

        // POST: PreOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "vehicle_id,customer_id,status")] pre_order pre_order)
        {
            if (ModelState.IsValid)
            {
                db.pre_order.Add(pre_order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "user_name", pre_order.customer_id);
            ViewBag.vehicle_id = new SelectList(db.vehicle_data, "vehicle_data_id", "model_number", pre_order.vehicle_id);
            return View(pre_order);
        }

        // GET: PreOrder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pre_order pre_order = db.pre_order.Find(id);
            if (pre_order == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "user_name", pre_order.customer_id);
            ViewBag.vehicle_id = new SelectList(db.vehicle_data, "vehicle_data_id", "model_number", pre_order.vehicle_id);
            return View(pre_order);
        }

        // POST: PreOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "vehicle_id,customer_id,status")] pre_order pre_order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pre_order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "user_name", pre_order.customer_id);
            ViewBag.vehicle_id = new SelectList(db.vehicle_data, "vehicle_data_id", "model_number", pre_order.vehicle_id);
            return View(pre_order);
        }

        // GET: PreOrder/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pre_order pre_order = db.pre_order.Find(id);
            if (pre_order == null)
            {
                return HttpNotFound();
            }
            return View(pre_order);
        }

        // POST: PreOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pre_order pre_order = db.pre_order.Find(id);
            db.pre_order.Remove(pre_order);
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
