using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RACKS_WAREHOUSEController : Controller
    {
        private WebApplication1Context db = new WebApplication1Context();

        // GET: RACKS_WAREHOUSE
        public ActionResult Index(string searchRacks,string searchRackTyp)
        {
            if (!searchRacks.IsNullOrWhiteSpace())
            {
                return View(db.RACKS_WAREHOUSE.Where(x => x.RACK_NUMBER.Equals(searchRacks)).ToList());
            }
            else if (!searchRackTyp.IsNullOrWhiteSpace())
            {
                return View(db.RACKS_WAREHOUSE.Where(x => x.RACK_TYPE.Equals(searchRackTyp)).ToList());
            }
            else
            {
                return View(db.RACKS_WAREHOUSE.ToList());
               
            }

        }

        // GET: RACKS_WAREHOUSE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RACKS_WAREHOUSE rACKS_WAREHOUSE = db.RACKS_WAREHOUSE.Find(id);
            if (rACKS_WAREHOUSE == null)
            {
                return HttpNotFound();
            }
            return View(rACKS_WAREHOUSE);
        }

        // GET: RACKS_WAREHOUSE/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RACKS_WAREHOUSE/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RACK_TYPE,RACK_NUMBER,CYCLE_COUNT_DATE")] RACKS_WAREHOUSE rACKS_WAREHOUSE)
        {
            if (ModelState.IsValid)
            {
                db.RACKS_WAREHOUSE.Add(rACKS_WAREHOUSE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rACKS_WAREHOUSE);
        }

        // GET: RACKS_WAREHOUSE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RACKS_WAREHOUSE rACKS_WAREHOUSE = db.RACKS_WAREHOUSE.Find(id);
            if (rACKS_WAREHOUSE == null)
            {
                return HttpNotFound();
            }
            return View(rACKS_WAREHOUSE);
        }

        // POST: RACKS_WAREHOUSE/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RACK_TYPE,RACK_NUMBER,CYCLE_COUNT_DATE")] RACKS_WAREHOUSE rACKS_WAREHOUSE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rACKS_WAREHOUSE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rACKS_WAREHOUSE);
        }

        // GET: RACKS_WAREHOUSE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RACKS_WAREHOUSE rACKS_WAREHOUSE = db.RACKS_WAREHOUSE.Find(id);
            if (rACKS_WAREHOUSE == null)
            {
                return HttpNotFound();
            }
            return View(rACKS_WAREHOUSE);
        }

        // POST: RACKS_WAREHOUSE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RACKS_WAREHOUSE rACKS_WAREHOUSE = db.RACKS_WAREHOUSE.Find(id);
            db.RACKS_WAREHOUSE.Remove(rACKS_WAREHOUSE);
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
