using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ShowroomManagement.Models;

namespace ShowroomManagement.Controllers
{
    public class UsersController : Controller
    {
        private showroomEntities db = new showroomEntities();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(user obj)
        {
            var customer = db.users
                .Include(u => u.group)
                .Include(u => u.organization)
                .FirstOrDefault(c => c.user_name == obj.user_name);
            if (customer != null)
            {
                if (HashPassword(obj.password) == customer.password.Trim())
                {
                    Session["EmployeeName"] = obj.first_name + " " + obj.last_name;
                    Session["EmployeeId"] = customer.user_id;
                    return RedirectToAction("Dashboard", "Home");
                }
                ViewBag.Message = ("Password is incorrect!");
            }
            ViewBag.Message = ("Password is incorrect!");
            return View(obj);
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ChangePasswordForm obj)
        {
            var id = (int)Session["EmployeeId"]; 
            var customer = db.users
                .Include(u => u.group)
                .FirstOrDefault(c => c.user_id == id);
            if (customer != null)
            {

                if (HashPassword(obj.oldPasswprd) == customer.password.Trim())
                {
                    if(obj.newPasswprd == obj.confirmPassword)
                    {
                        customer.password = HashPassword(obj.newPasswprd);
                        db.users.AddOrUpdate(customer);
                        db.SaveChanges();

                        return RedirectToAction("Details");
                    }
                    ViewBag.Message = "Confirm Password must be equal New Password!";
                    return View(obj);
                }
                ViewBag.Message = "Password is incorrect!";
            }
            return View(obj);
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        // GET: Users
        public ActionResult Index()
        {
            var users = db.users.Include(u => u.group).Include(u => u.organization).Include(u => u.user2);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            var userid = id == null ? (int)Session["EmployeeId"] : id; 
            user user = db.users.Find(userid);
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
                user.password = HashPassword(user.password.Trim());
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
                var userToUpdate = db.users.FirstOrDefault(u => u.user_id == user.user_id);

                if (userToUpdate != null)
                {
                    userToUpdate.user_name = user.user_name;
                    userToUpdate.first_name = user.first_name;
                    userToUpdate.last_name = user.last_name;
                    userToUpdate.phone_number = user.phone_number;
                    userToUpdate.address = user.address;
                    userToUpdate.manage_id = user.manage_id;
                    userToUpdate.group_id = user.group_id;
                    userToUpdate.organization_id = user.organization_id;

                    db.Entry(userToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Details");
                }
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
