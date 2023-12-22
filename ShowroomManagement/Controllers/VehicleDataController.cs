﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShowroomManagement.Models;

namespace ShowroomManagement.Controllers
{
    public class VehicleDataController : Controller
    {
        private showroomEntities db = new showroomEntities();

        // GET: VehicleData
        public ActionResult Index()
        {
            var vehicle_data = db.vehicle_data.Include(v => v.vehicle);
            return View(vehicle_data.ToList());
        }

        // GET: VehicleData/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vehicle_data vehicle_data = db.vehicle_data.Find(id);
            if (vehicle_data == null)
            {
                return HttpNotFound();
            }
            return View(vehicle_data);
        }

        // GET: VehicleData/Create
        public ActionResult Create()
        {
            ViewBag.model_number = new SelectList(db.vehicles, "model_number", "name");
            return View();
        }

        // POST: VehicleData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "vehicle_data_id,model_number,color,listId")] vehicle_data vehicle_data)
        {
            if (ModelState.IsValid)
            {
                var datasave = db.vehicle_data.Add(vehicle_data);
                db.SaveChanges();

                if (Request.Files["images"] != null)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];

                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/Assests/Image/"), fileName);
                            file.SaveAs(path);
                            var vehical_image = new vehicle_image();
                            vehical_image.image_url = fileName;
                            vehical_image.vehicle_id = datasave.vehicle_data_id;

                            db.vehicle_image.Add(vehical_image);
                        }
                    }
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.model_number = new SelectList(db.vehicles, "model_number", "name", vehicle_data.model_number);
            return View(vehicle_data);
        }

        // GET: VehicleData/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vehicle_data vehicle_data = db.vehicle_data.Find(id);
            if (vehicle_data == null)
            {
                return HttpNotFound();
            }
            ViewBag.model_number = new SelectList(db.vehicles, "model_number", "name", vehicle_data.model_number);
            return View(vehicle_data);
        }

        // POST: VehicleData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "vehicle_data_id,model_number,color,listId")] vehicle_data vehicle_data)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle_data).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.model_number = new SelectList(db.vehicles, "model_number", "name", vehicle_data.model_number);
            return View(vehicle_data);
        }

        // GET: VehicleData/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vehicle_data vehicle_data = db.vehicle_data.Find(id);
            if (vehicle_data == null)
            {
                return HttpNotFound();
            }
            return View(vehicle_data);
        }

        // POST: VehicleData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            vehicle_data vehicle_data = db.vehicle_data.Find(id);
            db.vehicle_data.Remove(vehicle_data);
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
