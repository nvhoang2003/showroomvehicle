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
    public class CustomerOrderController : Controller
    {
        private showroomEntities db = new showroomEntities();

        // GET: CustomerOrder
        public ActionResult Index()
        {
            var customer_order = db.customer_order.Include(c => c.customer).Include(c => c.order);
            return View(customer_order.ToList());
        }

        // GET: CustomerOrder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer_order customer_order = db.customer_order.Find(id);
            if (customer_order == null)
            {
                return HttpNotFound();
            }
            return View(customer_order);
        }

        // GET: CustomerOrder/Create
        public ActionResult Create()
        {
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "user_name");
            ViewBag.order_id = new SelectList(db.orders, "order_id", "status");
            return View();
        }

        // POST: CustomerOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "customer_id,order_id,created_at,updated_at")] customer_order customer_order)
        {
            if (ModelState.IsValid)
            {
                db.customer_order.Add(customer_order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "user_name", customer_order.customer_id);
            ViewBag.order_id = new SelectList(db.orders, "order_id", "status", customer_order.order_id);
            return View(customer_order);
        }

        // GET: CustomerOrder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer_order customer_order = db.customer_order.Find(id);
            if (customer_order == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "user_name", customer_order.customer_id);
            ViewBag.order_id = new SelectList(db.orders, "order_id", "status", customer_order.order_id);
            return View(customer_order);
        }

        // POST: CustomerOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "customer_id,order_id,created_at,updated_at")] customer_order customer_order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer_order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "user_name", customer_order.customer_id);
            ViewBag.order_id = new SelectList(db.orders, "order_id", "status", customer_order.order_id);
            return View(customer_order);
        }

        // GET: CustomerOrder/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer_order customer_order = db.customer_order.Find(id);
            if (customer_order == null)
            {
                return HttpNotFound();
            }
            return View(customer_order);
        }

        // POST: CustomerOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            customer_order customer_order = db.customer_order.Find(id);
            db.customer_order.Remove(customer_order);
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
