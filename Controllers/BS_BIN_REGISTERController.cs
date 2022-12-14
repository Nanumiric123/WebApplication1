using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BS_BIN_REGISTERController : Controller
    {
        private WebApplication1Context db = new WebApplication1Context();

        // GET: BS_BIN_REGISTER
        public ActionResult Index(string searchPart)
        {
            var result = from s in db.BS_BIN_REGISTER.Select(p => new newBS_BIN_REGISTER
            {
                ID = p.ID,
                STORAGE_BIN = p.STORAGE_BIN,
                PART_NO = p.PART_NO,
                BIN_END = p.BIN_END,
                BIN_START = p.BIN_START,
                REG_DATE = p.REG_DATE,
                DATE_REP = p.DATE_REP,
                REMARKS = p.REMARKS,
                PIC = p.PIC
            })
                         select s;

            if(String.IsNullOrEmpty(searchPart))
            {
                return View(result.ToList());
            }
            else
            {
                result = result.Where(x => x.PART_NO.Contains(searchPart));
                return View(result.ToList());
            }

        }

        // GET: BS_BIN_REGISTER/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BS_BIN_REGISTER bS_BIN_REGISTER = db.BS_BIN_REGISTER.Find(id);
            if (bS_BIN_REGISTER == null)
            {
                return HttpNotFound();
            }
            return View(bS_BIN_REGISTER);
        }

        // GET: BS_BIN_REGISTER/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BS_BIN_REGISTER/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,STORAGE_BIN,PART_NO,BIN_END,BIN_START,REG_DATE,DATE_REP,REMARKS,PIC")] BS_BIN_REGISTER bS_BIN_REGISTER)
        {
            Int32 binStartStr = Int32.Parse(Regex.Match(bS_BIN_REGISTER.BIN_START, @"\d+").Value);
            Int32 binENDStr = Int32.Parse(Regex.Match(bS_BIN_REGISTER.BIN_END, @"\d+").Value);
            Int32 binTotal = binENDStr - binStartStr;

            if (binTotal > 0)
            {
                for (int i = 0; i < binTotal; i++)
                {
                    int b = binStartStr + i + 1;
                    if (b < 10)
                    {
                        bS_BIN_REGISTER.STORAGE_BIN = "BS0000" + b.ToString();
                    }
                    else if (b < 100)
                    {
                        bS_BIN_REGISTER.STORAGE_BIN = "BS000" + b.ToString();
                    }
                    else if (b < 999)
                    {
                        bS_BIN_REGISTER.STORAGE_BIN = "BS00" + b.ToString();
                    }
                    else if (b < 10000)
                    {
                        bS_BIN_REGISTER.STORAGE_BIN = "BS0" + b.ToString();
                    }
                    else if (b < 100000)
                    {
                        bS_BIN_REGISTER.STORAGE_BIN = "BS" + b.ToString();
                    }
                    if (ModelState.IsValid)
                    {
                        db.BS_BIN_REGISTER.Add(bS_BIN_REGISTER);
                        db.SaveChanges();

                    }
                }
                return RedirectToAction("Index");


            }
            else
            {
                return RedirectToAction("Error");
            }
            


        }

        // GET: BS_BIN_REGISTER/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BS_BIN_REGISTER bS_BIN_REGISTER = db.BS_BIN_REGISTER.Find(id);
            if (bS_BIN_REGISTER == null)
            {
                return HttpNotFound();
            }
            return View(bS_BIN_REGISTER);
        }

        // POST: BS_BIN_REGISTER/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,STORAGE_BIN,PART_NO,BIN_END,BIN_START,REG_DATE,DATE_REP,REMARKS,PIC")] BS_BIN_REGISTER bS_BIN_REGISTER)
        {
            var result = from s in db.BS_BIN_REGISTER.Select(p => new newBS_BIN_REGISTER
            {
                ID = p.ID,
                STORAGE_BIN = p.STORAGE_BIN,
                PART_NO = p.PART_NO,
                BIN_END = p.BIN_END,
                BIN_START = p.BIN_START,
                REG_DATE = p.REG_DATE,
                DATE_REP = p.DATE_REP,
                REMARKS = p.REMARKS,
                PIC = p.PIC
            }).Where(x => x.PART_NO.Equals(bS_BIN_REGISTER.STORAGE_BIN))
                         select s;


            if (ModelState.IsValid)
            {
                db.Entry(bS_BIN_REGISTER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bS_BIN_REGISTER);
        }

        // GET: BS_BIN_REGISTER/Delete/5
        public ActionResult Delete(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BS_BIN_REGISTER bS_BIN_REGISTER = db.BS_BIN_REGISTER.Find(id);

            if (bS_BIN_REGISTER == null)
            {
                return HttpNotFound();
            }
            return View(bS_BIN_REGISTER);
        }

        // POST: BS_BIN_REGISTER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BS_BIN_REGISTER bS_BIN_REGISTER = db.BS_BIN_REGISTER.Find(id);

            var dbBinReg = from k in db.BS_BIN_REGISTER.Select(p => new BSBINDATA
            {
                ID = p.ID,
                PART_NO = p.PART_NO,
                STORAGE_BIN = p.STORAGE_BIN,
            }).Where(x => x.PART_NO.Equals(bS_BIN_REGISTER.PART_NO))
                           select k;
            foreach(var p in dbBinReg.ToList())
            {
                bS_BIN_REGISTER = db.BS_BIN_REGISTER.Find(p.ID);
                db.BS_BIN_REGISTER.Remove(bS_BIN_REGISTER);
                db.SaveChanges();
            }


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

        // GET: BS_BIN_REGISTER/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: BS_BIN_REGISTER/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "ID,STORAGE_BIN,PART_NO,BIN_END,BIN_START,REG_DATE,DATE_REP,REMARKS,PIC")] BS_BIN_REGISTER bS_BIN_REGISTER)
        {
            Int32 binStartStr = Int32.Parse(Regex.Match(bS_BIN_REGISTER.BIN_START, @"\d+").Value);
            Int32 binENDStr = Int32.Parse(Regex.Match(bS_BIN_REGISTER.BIN_END, @"\d+").Value);
            Int32 binTotal = (binENDStr - binStartStr) + 1;

            if (binTotal > 0)
            {
                List<string> bins = new List<string>();

                for (int i = binStartStr; i <= binENDStr; i++)
                {
                    string b = "";
                    if (i < 10)
                    {
                        b = "BS0000" + i.ToString();
                    }
                    else if (i < 100)
                    {
                        b = "BS000" + i.ToString();
                    }
                    else if (i < 999)
                    {
                        b = "BS00" + i.ToString();
                    }
                    else if (i < 10000)
                    {
                        b = "BS0" + i.ToString();
                    }
                    else if (i < 100000)
                    {
                        b = "BS" + i.ToString();
                    }
                    bins.Add(b);


                }

                var bsBinList = from s in db.BS_BIN_REGISTER.Select(p => new
                {
                    ID = p.ID,
                    STORAGE_BIN = p.STORAGE_BIN,
                    PART_NO = p.PART_NO,
                    BIN_END = p.BIN_END,
                    BIN_START = p.BIN_START,
                    REG_DATE = p.REG_DATE,
                    DATE_REP = p.DATE_REP,
                    REMARKS = p.REMARKS,
                    PIC = p.PIC

                }).Where(x => bins.Contains(x.STORAGE_BIN))
                                select s;

                foreach (var s in bsBinList.ToList())
                {
                    BS_BIN_REGISTER bSLineByRegister = new BS_BIN_REGISTER();

                    bSLineByRegister.ID = s.ID;
                    bSLineByRegister.STORAGE_BIN = s.STORAGE_BIN;
                    bSLineByRegister.BIN_START = bS_BIN_REGISTER.BIN_START;
                    bSLineByRegister.BIN_END = bS_BIN_REGISTER.BIN_END;
                    bSLineByRegister.PIC = bS_BIN_REGISTER.PIC;
                    bSLineByRegister.REMARKS = bS_BIN_REGISTER.REMARKS;
                    bSLineByRegister.DATE_REP = bS_BIN_REGISTER.DATE_REP;
                    bSLineByRegister.REG_DATE = bS_BIN_REGISTER.REG_DATE;
                    bSLineByRegister.PART_NO = bS_BIN_REGISTER.PART_NO;
                    if (ModelState.IsValid)
                    {
                        db.Entry(bSLineByRegister).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }



        }

        // GET: BS_BIN_REGISTER/Create
        public ActionResult Clear()
        {
            return View();
        }

        // POST: BS_BIN_REGISTER/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Clear([Bind(Include = "ID,STORAGE_BIN,PART_NO,BIN_END,BIN_START,REG_DATE,DATE_REP,REMARKS,PIC")] BS_BIN_REGISTER bS_BIN_REGISTER)
        {
            Int32 binStartStr = Int32.Parse(Regex.Match(bS_BIN_REGISTER.BIN_START, @"\d+").Value);
            Int32 binENDStr = Int32.Parse(Regex.Match(bS_BIN_REGISTER.BIN_END, @"\d+").Value);
            Int32 binTotal = binENDStr - binStartStr + 1;

            if (binTotal > 0)
            {
                List<string> bins = new List<string>();

                for (int i = binStartStr; i <= binENDStr; i++)
                {
                    string b = "";
                    if (i < 10)
                    {
                        b = "BS0000" + i.ToString();
                    }
                    else if (i < 100)
                    {
                        b = "BS000" + i.ToString();
                    }
                    else if (i < 999)
                    {
                        b = "BS00" + i.ToString();
                    }
                    else if (i < 10000)
                    {
                        b = "BS0" + i.ToString();
                    }
                    else if (i < 100000)
                    {
                        b = "BS" + i.ToString();
                    }
                    bins.Add(b);


                }

                var bsBinList = from s in db.BS_BIN_REGISTER.Select(p => new
                {
                    ID = p.ID,
                    STORAGE_BIN = p.STORAGE_BIN,
                    PART_NO = p.PART_NO,
                    BIN_END = p.BIN_END,
                    BIN_START = p.BIN_START,
                    REG_DATE = p.REG_DATE,
                    DATE_REP = p.DATE_REP,
                    REMARKS = p.REMARKS,
                    PIC = p.PIC

                }).Where(x => bins.Contains(x.STORAGE_BIN))
                                select s;

                foreach (var s in bsBinList.ToList())
                {
                    BS_BIN_REGISTER bSLineByRegister = new BS_BIN_REGISTER();

                    bSLineByRegister.ID = s.ID;
                    bSLineByRegister.STORAGE_BIN = s.STORAGE_BIN;
                    bSLineByRegister.BIN_START = "";
                    bSLineByRegister.BIN_END = "";
                    bSLineByRegister.PIC = "";
                    bSLineByRegister.REMARKS = "";
                    bSLineByRegister.DATE_REP = bS_BIN_REGISTER.DATE_REP;
                    bSLineByRegister.REG_DATE = bS_BIN_REGISTER.REG_DATE;
                    bSLineByRegister.PART_NO = "";
                    if (ModelState.IsValid)
                    {
                        db.Entry(bSLineByRegister).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }


        }

    }
}
