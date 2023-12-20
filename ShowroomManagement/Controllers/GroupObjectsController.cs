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
    public class GroupObjectsController : Controller
    {
        private showroomEntities db = new showroomEntities();

        // GET: GroupObjects
        public ActionResult Index()
        {
            var group_objects = db.group_objects.Include(g => g.group).Include(g => g.@object);
            return View(group_objects.ToList());
        }

        // GET: GroupObjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            group_objects group_objects = db.group_objects.Find(id);
            if (group_objects == null)
            {
                return HttpNotFound();
            }
            return View(group_objects);
        }

        // GET: GroupObjects/Create
        public ActionResult Create()
        {
            ViewBag.group_id = new SelectList(db.groups, "group_id", "name");
            ViewBag.object_id = new SelectList(db.objects1, "object_id", "name");
            return View();
        }

        // POST: GroupObjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "group_id,object_id,created_at,updated_at")] group_objects group_objects)
        {
            if (ModelState.IsValid)
            {
                db.group_objects.Add(group_objects);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.group_id = new SelectList(db.groups, "group_id", "name", group_objects.group_id);
            ViewBag.object_id = new SelectList(db.objects1, "object_id", "name", group_objects.object_id);
            return View(group_objects);
        }

        // GET: GroupObjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            group_objects group_objects = db.group_objects.Find(id);
            if (group_objects == null)
            {
                return HttpNotFound();
            }
            ViewBag.group_id = new SelectList(db.groups, "group_id", "name", group_objects.group_id);
            ViewBag.object_id = new SelectList(db.objects1, "object_id", "name", group_objects.object_id);
            return View(group_objects);
        }

        // POST: GroupObjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "group_id,object_id,created_at,updated_at")] group_objects group_objects)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group_objects).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.group_id = new SelectList(db.groups, "group_id", "name", group_objects.group_id);
            ViewBag.object_id = new SelectList(db.objects1, "object_id", "name", group_objects.object_id);
            return View(group_objects);
        }

        // GET: GroupObjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            group_objects group_objects = db.group_objects.Find(id);
            if (group_objects == null)
            {
                return HttpNotFound();
            }
            return View(group_objects);
        }

        // POST: GroupObjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            group_objects group_objects = db.group_objects.Find(id);
            db.group_objects.Remove(group_objects);
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
