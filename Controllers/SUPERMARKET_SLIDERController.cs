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
    [NoDirectAccess]
    public class SUPERMARKET_SLIDERController : Controller
    {
        private WebApplication1Context db = new WebApplication1Context();

        // GET: SUPERMARKET_SLIDER
        public ActionResult Index(string SearchColor,string SearchRack,string SearchSliderAddress, string SearchPartNumber,string SearchBIN,string SearchArea,string SearchStatus)
        {
            var sliderList = db.SUPERMARKET_SLIDER.ToList();

            if (!String.IsNullOrEmpty(SearchColor))
            { 
                 sliderList = db.SUPERMARKET_SLIDER.Where(x => x.COLOR.Equals(SearchColor)).ToList();
            }
            else if (!String.IsNullOrEmpty(SearchStatus))
            {
                sliderList = db.SUPERMARKET_SLIDER.Where(x => x.STATUS.Equals(SearchStatus)).ToList();
            }
            else if (!String.IsNullOrEmpty(SearchArea))
            {
                sliderList = db.SUPERMARKET_SLIDER.Where(x => x.AREA.Equals(SearchArea)).ToList();
            }
            else if (!String.IsNullOrEmpty(SearchBIN))
            {
                sliderList = db.SUPERMARKET_SLIDER.Where(x => x.BIN.Equals(SearchBIN)).ToList();
            }
            else if (!String.IsNullOrEmpty(SearchRack))
            {
                sliderList = db.SUPERMARKET_SLIDER.Where(x => x.RACK.Equals(SearchRack)).ToList();
            }
            else if (!String.IsNullOrEmpty(SearchPartNumber))
            {
                sliderList = db.SUPERMARKET_SLIDER.Where(x => x.PART_NUMBER.Equals(SearchPartNumber)).ToList();
            }
            else if (!String.IsNullOrEmpty(SearchSliderAddress))
            {
                sliderList = db.SUPERMARKET_SLIDER.Where(x => x.SLIDER_ADDRESS.Equals(SearchSliderAddress)).ToList();
            }
                return View(sliderList.OrderBy(s => s.SLIDER_ADDRESS).ToList());
        }

        // GET: SUPERMARKET_SLIDER/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPERMARKET_SLIDER sUPERMARKET_SLIDER = db.SUPERMARKET_SLIDER.Find(id);
            if (sUPERMARKET_SLIDER == null)
            {
                return HttpNotFound();
            }
            return View(sUPERMARKET_SLIDER);
        }

        // GET: SUPERMARKET_SLIDER/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SUPERMARKET_SLIDER/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SLIDER_ADDRESS,PART_NUMBER,STATUS,RACK,AREA,BIN,COLOR,MULTIPLE_PLART")] SUPERMARKET_SLIDER sUPERMARKET_SLIDER)
        {
            if (ModelState.IsValid)
            {
                db.SUPERMARKET_SLIDER.Add(sUPERMARKET_SLIDER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sUPERMARKET_SLIDER);
        }

        // GET: SUPERMARKET_SLIDER/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPERMARKET_SLIDER sUPERMARKET_SLIDER = db.SUPERMARKET_SLIDER.Find(id);
            if (sUPERMARKET_SLIDER == null)
            {
                return HttpNotFound();
            }
            return View(sUPERMARKET_SLIDER);
        }



        // POST: SUPERMARKET_SLIDER/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SLIDER_ADDRESS,PART_NUMBER,STATUS,RACK,AREA,BIN,COLOR,MULTIPLE_PLART")] SUPERMARKET_SLIDER sUPERMARKET_SLIDER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sUPERMARKET_SLIDER).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("sliderAddress", "KANBAN_MASTER");
            }
            //return View(sUPERMARKET_SLIDER);
            return RedirectToAction("sliderAddress", "KANBAN_MASTER");
        }

        // GET: SUPERMARKET_SLIDER/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPERMARKET_SLIDER sUPERMARKET_SLIDER = db.SUPERMARKET_SLIDER.Find(id);
            if (sUPERMARKET_SLIDER == null)
            {
                return HttpNotFound();
            }
            return View(sUPERMARKET_SLIDER);
        }

        // POST: SUPERMARKET_SLIDER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SUPERMARKET_SLIDER sUPERMARKET_SLIDER = db.SUPERMARKET_SLIDER.Find(id);
            db.SUPERMARKET_SLIDER.Remove(sUPERMARKET_SLIDER);
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
