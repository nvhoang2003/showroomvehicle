using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using ShowroomManagement.Models;

namespace ShowroomManagement.Controllers
{
    public class GroupsController : Controller
    {
        private showroomEntities db = new showroomEntities();

        // GET: Groups
        public ActionResult Index()
        {
            return View(db.groups.ToList());
        }

        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            group group = db.groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            ViewBag.object_id = new SelectList(db.objects1, "object_id", "name");
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name,group_id,description")] group group, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var groupSvae = db.groups.Add(group);
                db.SaveChanges();

                var selectedItems = form["selectedItems"].Split(',');

                foreach (var item in selectedItems)
                {
                    var go = new group_objects();
                    int number;
                    bool success = int.TryParse(item,out number);

                    if(success == true)
                    {
                        go.group_id = groupSvae.group_id;
                        go.object_id = number;

                        db.group_objects.Add(go);
                    }                   
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.object_id = new SelectList(db.objects1, "object_id", "name");
            group group = db.groups.Find(id);
            ViewBag.object_choosen = db.group_objects.Where(go => go.group_id == id).Select(go => go.object_id).ToList();

            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "name,group_id,description")] group group,FormCollection form)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();

                List<group_objects> group_objects = db.group_objects.Where(go => go.group_id == group.group_id).ToList();
                db.group_objects.RemoveRange(group_objects);

                var selectedItems = form["selectedItems"].Split(',');

                foreach (var item in selectedItems)
                {
                    var go = new group_objects();
                    int number;
                    bool success = int.TryParse(item, out number);

                    if (success == true)
                    {
                        go.group_id = group.group_id;
                        go.object_id = number;

                        db.group_objects.Add(go);
                    }
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            group group = db.groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try{
                group group = db.groups.Find(id);
                db.groups.Remove(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex){
                var vehicle = db.groups.Find(id);
                ViewBag.Message = "Group Has Some User. Can't delete it";
                return View(vehicle);
            }
           
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
