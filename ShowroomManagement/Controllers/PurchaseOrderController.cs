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
    public class PurchaseOrderController : Controller
    {
        private showroomEntities db = new showroomEntities();

        // GET: PurchaseOrder
        public ActionResult Index()
        {
            var purchase_order = db.purchase_order.Include(p => p.user);
            return View(purchase_order.ToList());
        }

        // GET: PurchaseOrder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            purchase_order purchase_order = db.purchase_order.Find(id);
            if (purchase_order == null)
            {
                return HttpNotFound();
            }
            return View(purchase_order);
        }

        // GET: PurchaseOrder/Create
        public ActionResult Create()
        {
            ViewBag.manage_by = new SelectList(db.users, "user_id", "user_name");
            return View();
        }

        // POST: PurchaseOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "purchase_id,date_purchase,manage_by")] purchase_order purchase_order)
        {
            if (ModelState.IsValid)
            {
                db.purchase_order.Add(purchase_order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.manage_by = new SelectList(db.users, "user_id", "user_name", purchase_order.manage_by);
            return View(purchase_order);
        }

        // GET: PurchaseOrder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            purchase_order purchase_order = db.purchase_order.Find(id);
            if (purchase_order == null)
            {
                return HttpNotFound();
            }
            ViewBag.manage_by = new SelectList(db.users, "user_id", "user_name", purchase_order.manage_by);
            return View(purchase_order);
        }

        // POST: PurchaseOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "purchase_id,date_purchase,manage_by")] purchase_order purchase_order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchase_order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.manage_by = new SelectList(db.users, "user_id", "user_name", purchase_order.manage_by);
            return View(purchase_order);
        }

        // GET: PurchaseOrder/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            purchase_order purchase_order = db.purchase_order.Find(id);
            if (purchase_order == null)
            {
                return HttpNotFound();
            }
            return View(purchase_order);
        }

        // POST: PurchaseOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            purchase_order purchase_order = db.purchase_order.Find(id);
            db.purchase_order.Remove(purchase_order);
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
