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
    public class ServiceOrderController : Controller
    {
        private showroomEntities db = new showroomEntities();

        public ActionResult Home()
        {
            return View();
        }
        // GET: ServiceOrder
        public ActionResult Index()
        {
            var service_order = db.service_order.Include(s => s.customer).Include(s => s.user);
            return View(service_order.ToList());
        }

        // GET: ServiceOrder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            service_order service_order = db.service_order.Find(id);
            if (service_order == null)
            {
                return HttpNotFound();
            }
            return View(service_order);
        }
        // GET: ServiceOrder/Create
        public ActionResult Create()
        {
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "user_name");
            ViewBag.manage_by = new SelectList(db.users, "user_id", "user_name");
            return View();
        }

        // POST: ServiceOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "service_id,name,time_create,description,manage_by,customer_id")] service_order service_order)
        {
            if (ModelState.IsValid)
            {
                db.service_order.Add(service_order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "user_name", service_order.customer_id);
            ViewBag.manage_by = new SelectList(db.users, "user_id", "user_name", service_order.manage_by);
            return View(service_order);
        }

        // GET: ServiceOrder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            service_order service_order = db.service_order.Find(id);
            if (service_order == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "user_name", service_order.customer_id);
            ViewBag.manage_by = new SelectList(db.users, "user_id", "user_name", service_order.manage_by);
            return View(service_order);
        }

        // POST: ServiceOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "service_id,name,time_create,description,manage_by,customer_id")] service_order service_order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service_order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "user_name", service_order.customer_id);
            ViewBag.manage_by = new SelectList(db.users, "user_id", "user_name", service_order.manage_by);
            return View(service_order);
        }

        // GET: ServiceOrder/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            service_order service_order = db.service_order.Find(id);
            if (service_order == null)
            {
                return HttpNotFound();
            }
            return View(service_order);
        }

        // POST: ServiceOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            service_order service_order = db.service_order.Find(id);
            db.service_order.Remove(service_order);
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
