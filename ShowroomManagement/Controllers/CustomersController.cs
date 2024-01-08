using Microsoft.AspNetCore.Identity;
using ShowroomManagement.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace ShowroomManagement.Controllers
{
    public class CustomersController : Controller
    {
        private showroomEntities db = new showroomEntities();

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ChangePasswordForm obj)
        {
            var id = (int)Session["CustomerId"];
            var customer = db.customers
                .FirstOrDefault(c => c.customer_id == id);
            if (customer != null)
            {
                if (HashPassword(obj.oldPasswprd) == customer.password.Trim())
                {
                    if (obj.newPasswprd == obj.confirmPassword)
                    {
                        customer.password = HashPassword(obj.newPasswprd);
                        db.customers.AddOrUpdate(customer);
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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(customer obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            var customer = db.customers.FirstOrDefault(c => c.user_name == obj.user_name || c.email == obj.user_name);
            if (customer != null)
            {
                var passwordVerificationResult = Crypto.VerifyHashedPassword(customer.password, obj.password);
                if (passwordVerificationResult == true)
                {
                    Session["CustomerName"] = customer.user_name;
                    Session["CustomerId"] = customer.customer_id;
                    return RedirectToAction("Index", "Home");
                }
                TempData["ErrorMessage"] = ("Password is incorrect!");
            }
            return View(obj);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register (customer obj)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.customers.FirstOrDefault(c => c.user_name == obj.user_name);
                if(existingUser == null)
                {
                    obj.password = Crypto.HashPassword(obj.password);
                    db.customers.Add(obj);
                    db.SaveChanges();

                    Session["CustomerName"] = obj.user_name;
                    Session["CustomerId"] = obj.customer_id;

                    return RedirectToAction("Index", "Home");
                }
            }
            return View (obj);
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
        // GET: Customers
        public ActionResult Index()
        {
            return View(db.customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "customer_id,user_name,password,first_name,last_name,date_of_birth,phone_number,email")] customer customer)
        {
            if (ModelState.IsValid)
            {
                db.customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "customer_id,user_name,first_name,last_name,date_of_birth,phone_number,email")] customer customer)
        {
            if (ModelState.IsValid)
            {
                var userToUpdate = db.customers.FirstOrDefault(u => u.customer_id == customer.customer_id);

                if (userToUpdate != null)
                {
                    userToUpdate.user_name = customer.user_name;
                    userToUpdate.first_name = customer.first_name;
                    userToUpdate.last_name = customer.last_name;
                    userToUpdate.phone_number = customer.phone_number;
                    userToUpdate.date_of_birth = customer.date_of_birth;
                    userToUpdate.email = customer.email;

                    db.Entry(userToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Details");
                }
                //customer customerToUpdate = db.customers.Find(customer.customer_id);
                //customer.password = customerToUpdate.password;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            customer customer = db.customers.Find(id);
            db.customers.Remove(customer);
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
