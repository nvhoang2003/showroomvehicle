using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
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

        public ActionResult DeleteItem(int vehicleId, int purchaseId)
        {
            var purchaseToRemove = db.vehicle_purchase.Where(vp => vp.vehicle_id == vehicleId && vp.purchase_order_id == purchaseId).FirstOrDefault();

            db.vehicle_purchase.Remove(purchaseToRemove);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = purchaseId });
        }

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

            string Sql = "SELECT dbo.vehicle_purchase.purchase_order_id, dbo.[user].first_name, dbo.[user].last_name, dbo.purchase_order.date_purchase, dbo.vehicle_purchase.quantity, dbo.vehicle_purchase.price, dbo.vehicle_data.model_number, dbo.vehicle_data.color, dbo.purchase_order.purchase_id, dbo.vehicle_purchase.vehicle_id FROM dbo.vehicle_data INNER JOIN dbo.vehicle_purchase ON dbo.vehicle_data.vehicle_data_id = dbo.vehicle_purchase.vehicle_id INNER JOIN dbo.purchase_order ON dbo.vehicle_purchase.purchase_order_id = dbo.purchase_order.purchase_id INNER JOIN dbo.[user] ON dbo.purchase_order.manage_by = dbo.[user].user_id";

            string entityConnectionString = ConfigurationManager.ConnectionStrings["showroomEntities"].ConnectionString;
            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder(entityConnectionString);
            string providerConnectionString = entityBuilder.ProviderConnectionString;

            SqlConnection cnn = new SqlConnection(providerConnectionString);

            SqlDataAdapter sa = new SqlDataAdapter(Sql, cnn);
            DataTable dt = new DataTable();
            sa.Fill(dt);

            List<PurchaseResposneDTO> itemPurchase = new List<PurchaseResposneDTO>();

            PurchaseResposneDTO c = new PurchaseResposneDTO();
            foreach (DataRow item in dt.Rows)
            {
                c = new PurchaseResposneDTO();
                c.purchaseId = (int)item["purchase_order_id"];
                c.vehicleId = (int)item["vehicle_id"];
                c.fullName = item["first_name"].ToString() + " " + item["last_name"].ToString();
                c.datePurchase = (DateTime)item["date_purchase"];
                c.quantity = (int)item["quantity"];
                c.price = (double)item["price"];
                c.modelNumber = item["model_number"].ToString();
                c.color = item["color"].ToString();
                itemPurchase.Add(c);
            }

            ViewBag.item_purchase = itemPurchase;

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
                purchase_order.manage_by = (int)Session["EmployeeId"];
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
