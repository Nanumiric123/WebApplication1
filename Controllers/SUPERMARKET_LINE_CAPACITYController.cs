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

namespace WebApplication1.Views
{
    public class SUPERMARKET_LINE_CAPACITYController : Controller
    {
        private capacityContext db = new capacityContext();
        private WebApplication1Context kanbandb = new WebApplication1Context();

        // GET: SUPERMARKET_LINE_CAPACITY
        public ActionResult Index()
        {
            return View(db.SUPERMARKET_LINE_CAPACITY.ToList());
        }

        // GET: SUPERMARKET_LINE_CAPACITY/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPERMARKET_LINE_CAPACITY sUPERMARKET_LINE_CAPACITY = db.SUPERMARKET_LINE_CAPACITY.Find(id);
            if (sUPERMARKET_LINE_CAPACITY == null)
            {
                return HttpNotFound();
            }
            return View(sUPERMARKET_LINE_CAPACITY);
        }

        // GET: SUPERMARKET_LINE_CAPACITY/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SUPERMARKET_LINE_CAPACITY/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CUSTOMER,H0,A0,MODEL,CAPACITY,MINUTE_30,MINUTE_60,MINUTE_90")] SUPERMARKET_LINE_CAPACITY sUPERMARKET_LINE_CAPACITY)
        {
            change_kanban_master_kanban_qtyA0(sUPERMARKET_LINE_CAPACITY.A0);
            change_kanban_master_kanban_qtyH0(sUPERMARKET_LINE_CAPACITY.H0);

            if (ModelState.IsValid)
            {

                db.SUPERMARKET_LINE_CAPACITY.Add(sUPERMARKET_LINE_CAPACITY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sUPERMARKET_LINE_CAPACITY);
        }
        private int calculate_prod_kanban_qty(decimal line_output, decimal bin_qty, decimal per_usage)
        {
            int result = 0;

            if (bin_qty > 100)
            {
                result = 3;

            }
            else
            {
                decimal output_perhr = (line_output / 10.5m) / bin_qty;
                decimal stock_60min = output_perhr * per_usage;

                result = int.Parse(Math.Floor(stock_60min).ToString()) + 2;
            }

            return result;
        }

        private int calculate_store_kanban_qty(decimal line_output, decimal bin_qty, decimal per_usage)
        {
            int result = 0;

            decimal output_perhr = line_output / 10.5m;
            decimal output_perhr_half = (line_output / 10.5m) / 2.0m;

            decimal SUPERMARKET_STOCK_90_MIN = ((output_perhr + output_perhr_half) / bin_qty) * per_usage;
            result = (int)Math.Ceiling(SUPERMARKET_STOCK_90_MIN);

            //result = int.Parse(Math.Round(SUPERMARKET_STOCK_90_MIN,1).ToString());
            result = result + 5;

            return result;
        }

        public void change_kanban_master_kanban_qtyA0(string A0)
        {
            var kanbanMasterdataA0 = kanbandb.KANBAN_MASTER.Where(s => s.LINE.Contains(A0));



            foreach (var kanbancard in kanbanMasterdataA0)
            {

            }

        }

        public void change_kanban_master_kanban_qtyH0(string H0)
        {
            var kanbanMasterdataA0 = kanbandb.KANBAN_MASTER.Where(s => s.LINE.Contains(H0));


        }

        // GET: SUPERMARKET_LINE_CAPACITY/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPERMARKET_LINE_CAPACITY sUPERMARKET_LINE_CAPACITY = db.SUPERMARKET_LINE_CAPACITY.Find(id);
            if (sUPERMARKET_LINE_CAPACITY == null)
            {
                return HttpNotFound();
            }
            return View(sUPERMARKET_LINE_CAPACITY);
        }

        // POST: SUPERMARKET_LINE_CAPACITY/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CUSTOMER,H0,A0,MODEL,CAPACITY,MINUTE_30,MINUTE_60,MINUTE_90")] SUPERMARKET_LINE_CAPACITY sUPERMARKET_LINE_CAPACITY)
        {
            change_kanban_master_kanban_qtyA0(sUPERMARKET_LINE_CAPACITY.A0);
            change_kanban_master_kanban_qtyH0(sUPERMARKET_LINE_CAPACITY.H0);

            if (ModelState.IsValid)
            {
                db.Entry(sUPERMARKET_LINE_CAPACITY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sUPERMARKET_LINE_CAPACITY);
        }

        // GET: SUPERMARKET_LINE_CAPACITY/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPERMARKET_LINE_CAPACITY sUPERMARKET_LINE_CAPACITY = db.SUPERMARKET_LINE_CAPACITY.Find(id);
            if (sUPERMARKET_LINE_CAPACITY == null)
            {
                return HttpNotFound();
            }
            return View(sUPERMARKET_LINE_CAPACITY);
        }

        // POST: SUPERMARKET_LINE_CAPACITY/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SUPERMARKET_LINE_CAPACITY sUPERMARKET_LINE_CAPACITY = db.SUPERMARKET_LINE_CAPACITY.Find(id);
            db.SUPERMARKET_LINE_CAPACITY.Remove(sUPERMARKET_LINE_CAPACITY);
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
