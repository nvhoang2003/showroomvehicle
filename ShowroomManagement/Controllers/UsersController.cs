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
    public class UsersController : Controller
    {
        private showroomEntities db = new showroomEntities();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.users.Include(u => u.group).Include(u => u.organization).Include(u => u.user2);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.group_id = new SelectList(db.groups, "group_id", "name");
            ViewBag.organization_id = new SelectList(db.organizations, "organization_id", "name");
            ViewBag.manage_id = new SelectList(db.users, "user_id", "user_name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "user_id,user_name,password,first_name,last_name,phone_number,address,manage_id,group_id,organization_id")] user user)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.group_id = new SelectList(db.groups, "group_id", "name", user.group_id);
            ViewBag.organization_id = new SelectList(db.organizations, "organization_id", "name", user.organization_id);
            ViewBag.manage_id = new SelectList(db.users, "user_id", "user_name", user.manage_id);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.group_id = new SelectList(db.groups, "group_id", "name", user.group_id);
            ViewBag.organization_id = new SelectList(db.organizations, "organization_id", "name", user.organization_id);
            ViewBag.manage_id = new SelectList(db.users, "user_id", "user_name", user.manage_id);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,user_name,password,first_name,last_name,phone_number,address,manage_id,group_id,organization_id")] user user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.group_id = new SelectList(db.groups, "group_id", "name", user.group_id);
            ViewBag.organization_id = new SelectList(db.organizations, "organization_id", "name", user.organization_id);
            ViewBag.manage_id = new SelectList(db.users, "user_id", "user_name", user.manage_id);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user user = db.users.Find(id);
            db.users.Remove(user);
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
