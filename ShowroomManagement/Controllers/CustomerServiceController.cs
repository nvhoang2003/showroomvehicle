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
    public class CustomerServiceController : Controller
    {
        private showroomEntities db = new showroomEntities();

        // GET: CustomerService
        public ActionResult Index()
        {
            var customer_service = db.customer_service.Include(c => c.customer).Include(c => c.service_order);
            return View(customer_service.ToList());
        }

        // GET: CustomerService/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer_service customer_service = db.customer_service.Find(id);
            if (customer_service == null)
            {
                return HttpNotFound();
            }
            return View(customer_service);
        }

        // GET: CustomerService/Create
        public ActionResult Create()
        {
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "user_name");
            ViewBag.service_id = new SelectList(db.service_order, "service_id", "name");
            return View();
        }

        // POST: CustomerService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "service_id,customer_id,created_at,updated_at")] customer_service customer_service)
        {
            if (ModelState.IsValid)
            {
                db.customer_service.Add(customer_service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "user_name", customer_service.customer_id);
            ViewBag.service_id = new SelectList(db.service_order, "service_id", "name", customer_service.service_id);
            return View(customer_service);
        }

        // GET: CustomerService/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer_service customer_service = db.customer_service.Find(id);
            if (customer_service == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "user_name", customer_service.customer_id);
            ViewBag.service_id = new SelectList(db.service_order, "service_id", "name", customer_service.service_id);
            return View(customer_service);
        }

        // POST: CustomerService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "service_id,customer_id,created_at,updated_at")] customer_service customer_service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer_service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "user_name", customer_service.customer_id);
            ViewBag.service_id = new SelectList(db.service_order, "service_id", "name", customer_service.service_id);
            return View(customer_service);
        }

        // GET: CustomerService/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer_service customer_service = db.customer_service.Find(id);
            if (customer_service == null)
            {
                return HttpNotFound();
            }
            return View(customer_service);
        }

        // POST: CustomerService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            customer_service customer_service = db.customer_service.Find(id);
            db.customer_service.Remove(customer_service);
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
