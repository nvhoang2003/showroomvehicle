using ShowroomManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShowroomManagement.Controllers
{
    public class HomeController : Controller
    {
        private showroomEntities db = new showroomEntities();

        public ActionResult Test() {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            if(Session["EmployeeId"] == null)
            {
                return RedirectToAction("Login", "Users");
            }

            int employeeId = (int)Session["EmployeeId"];

            var user = db.users
                .Include(u => u.group)
                .Include(u => u.organization)
                .Include(u => u.user2)
                .FirstOrDefault(u => u.user_id == employeeId);

            ViewBag.userdata = user;

            DateTime today = DateTime.Today;
            DateTime lastMonth = today.AddMonths(-1);
            DateTime firstDayOfLastMonth = new DateTime(lastMonth.Year, lastMonth.Month, 1);
            DateTime lastDayOfLastMonth = firstDayOfLastMonth.AddMonths(1).AddDays(-1);

            var purchaseLastMonth = db.purchase_order
                .Include(po => po.vehicle_purchase)
                .Where(po => po.date_purchase > firstDayOfLastMonth && po.date_purchase < lastDayOfLastMonth)
                .ToList();

            var orderLastMonth = db.orders
                .Include(o => o.vehicle_order)
                .Where(po => po.time_create > firstDayOfLastMonth && po.time_create < lastDayOfLastMonth)
                .ToList();

            int yourNumberOfOrder = orderLastMonth.Where(o => o.manage_by == employeeId).Count();
            double? totalSale = orderLastMonth.Sum(po => po.price);
            double? totalBuy = purchaseLastMonth.Sum(po => po.vehicle_purchase.Sum(item => item.price));

            ViewBag.yourNumberOfOrder = yourNumberOfOrder;
            ViewBag.totalSell = totalSale;
            ViewBag.totalBuy = totalBuy;

            return View();
        }

        public ActionResult About()

        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}