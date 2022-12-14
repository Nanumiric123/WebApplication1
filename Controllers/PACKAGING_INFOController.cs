using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PACKAGING_INFOController : Controller
    {
        private WebApplication1Context db = new WebApplication1Context();

        // GET: PACKAGING_INFO
        public ActionResult Index(string SearchpartNum,string extSearchPackMthd,string intSearchPackMthd)
        {
            var dbIndex = from s in db.PACKAGING_INFO.Select(p => new PACKAGING_INFO_INDEX
            {
                ID = p.ID,
                PART_NUMBER = p.PART_NUMBER,
                EXT_PCK_METHOD = p.EXT_PCK_METHOD,
                INT_PACK_METHOD = p.INT_PACK_METHOD,
                EXT_PCK_HEIGHT = p.EXT_PCK_HEIGHT,
                EXT_PCK_WIDTH = p.EXT_PCK_WIDTH,
                EXT_PCK_LENGTH = p.EXT_PCK_LENGTH,
                INT_PCK_HEIGHT = p.INT_PCK_HEIGHT,
                INT_PCK_WIDTH = p.INT_PCK_WIDTH,
                INT_PCK_LENGTH = p.INT_PCK_LENGTH,
                INT_PCK_QTY = p.INT_PCK_QTY,
                TOTAL_NUMBER_OF_INT = p.TOTAL_NUMBER_OF_INT,
                PCK_TOTAL_QTY = p.PCK_TOTAL_QTY,
                WEIGHT_KG = p.WEIGHT_KG,
                REMARKS = p.REMARKS,
                SAVE = p.SAVE
            })
                          select s;

            if (!String.IsNullOrEmpty(SearchpartNum))
            {
                dbIndex = dbIndex.Where(s => s.PART_NUMBER.Contains(SearchpartNum));

            }

            if (!String.IsNullOrEmpty(extSearchPackMthd))
            {
                dbIndex = dbIndex.Where(s => s.EXT_PCK_METHOD.Contains(extSearchPackMthd));

            }

            if (!String.IsNullOrEmpty(intSearchPackMthd))
            {
                dbIndex = dbIndex.Where(s => s.INT_PACK_METHOD.Contains(extSearchPackMthd));

            }

            return View(dbIndex.ToList());
        }

        // GET: PACKAGING_INFO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACKAGING_INFO pACKAGING_INFO = db.PACKAGING_INFO.Find(id);
            if (pACKAGING_INFO == null)
            {
                return HttpNotFound();
            }
            return View(pACKAGING_INFO);
        }

        // GET: PACKAGING_INFO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PACKAGING_INFO/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PART_NUMBER,EXT_PCK_METHOD,INT_PACK_METHOD,EXT_PCK_HEIGHT,EXT_PCK_WIDTH,EXT_PCK_LENGTH,INT_PCK_HEIGHT,INT_PCK_WIDTH,INT_PCK_LENGTH,INT_PCK_QTY,TOTAL_NUMBER_OF_INT,PCK_TOTAL_QTY,WEIGHT_KG,REMARKS,SAVE")] PACKAGING_INFO pACKAGING_INFO,
            HttpPostedFileBase photoA, HttpPostedFileBase photoB, HttpPostedFileBase photoC)
        {
            if (photoA != null)
            {
                MemoryStream target = new MemoryStream();
                photoA.InputStream.CopyTo(target);
                byte[] data = target.ToArray();
                pACKAGING_INFO.PHOTO_A = data;
            }
            else
            {
                pACKAGING_INFO.PHOTO_A = null;
            }

            if (photoB != null)
            {
                MemoryStream target = new MemoryStream();
                photoB.InputStream.CopyTo(target);
                byte[] data = target.ToArray();
                pACKAGING_INFO.PHOTO_B = data;
            }
            else
            {
                pACKAGING_INFO.PHOTO_B = null;
            }

            if (photoC != null)
            {
                MemoryStream target = new MemoryStream();
                photoC.InputStream.CopyTo(target);
                byte[] data = target.ToArray();
                pACKAGING_INFO.PHOTO_C = data;
            }
            else
            {
                pACKAGING_INFO.PHOTO_C = null;
            }

            if (ModelState.IsValid)
            {
                db.PACKAGING_INFO.Add(pACKAGING_INFO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pACKAGING_INFO);
        }

        // GET: PACKAGING_INFO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACKAGING_INFO pACKAGING_INFO = db.PACKAGING_INFO.Find(id);
            TempData["photoA"] = pACKAGING_INFO.PHOTO_A;
            Session["photoA"] = pACKAGING_INFO.PHOTO_A;
            TempData["photoB"] = pACKAGING_INFO.PHOTO_B;
            Session["photoB"] = pACKAGING_INFO.PHOTO_B;
            TempData["photoC"] = pACKAGING_INFO.PHOTO_C;
            Session["photoC"] = pACKAGING_INFO.PHOTO_C;
            if (pACKAGING_INFO == null)
            {
                return HttpNotFound();
            }
            return View(pACKAGING_INFO);
        }

        // POST: PACKAGING_INFO/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PART_NUMBER,EXT_PCK_METHOD,INT_PACK_METHOD,EXT_PCK_HEIGHT,EXT_PCK_WIDTH,EXT_PCK_LENGTH,INT_PCK_HEIGHT,INT_PCK_WIDTH,INT_PCK_LENGTH,INT_PCK_QTY,TOTAL_NUMBER_OF_INT,PCK_TOTAL_QTY,WEIGHT_KG,REMARKS,SAVE")] PACKAGING_INFO pACKAGING_INFO,
            HttpPostedFileBase photoA, HttpPostedFileBase photoB, HttpPostedFileBase photoC)
        {
            if (photoA != null)
            {
                MemoryStream target = new MemoryStream();
                photoA.InputStream.CopyTo(target);
                byte[] data = target.ToArray();
                pACKAGING_INFO.PHOTO_A = data;
            }
            else
            {
                if (TempData["photoA"] != null)
                {
                    byte[] photo_temp = (byte[])TempData["photoA"];
                    pACKAGING_INFO.PHOTO_A = photo_temp;
                }
                else
                {
                    byte[] photo_temp = (byte[])Session["photoA"];
                    pACKAGING_INFO.PHOTO_A = photo_temp;
                }

            }

            if (photoB != null)
            {
                MemoryStream target = new MemoryStream();
                photoB.InputStream.CopyTo(target);
                byte[] data = target.ToArray();
                pACKAGING_INFO.PHOTO_B = data;
            }
            else
            {
                if (TempData["photoB"] != null)
                {
                    byte[] photo_temp = (byte[])TempData["photoB"];
                    pACKAGING_INFO.PHOTO_B = photo_temp;
                }
                else
                {
                    byte[] photo_temp = (byte[])Session["photoB"];
                    pACKAGING_INFO.PHOTO_B = photo_temp;
                }

            }

            if (photoC != null)
            {
                MemoryStream target = new MemoryStream();
                photoC.InputStream.CopyTo(target);
                byte[] data = target.ToArray();
                pACKAGING_INFO.PHOTO_C = data;
            }
            else
            {
                if (TempData["photoC"] != null)
                {
                    byte[] photo_temp = (byte[])TempData["photoC"];
                    pACKAGING_INFO.PHOTO_C = photo_temp;
                }
                else
                {
                    byte[] photo_temp = (byte[])Session["photoC"];
                    pACKAGING_INFO.PHOTO_C = photo_temp;
                }

            }

            if (ModelState.IsValid)
            {
                db.Entry(pACKAGING_INFO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pACKAGING_INFO);
        }

        // GET: PACKAGING_INFO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACKAGING_INFO pACKAGING_INFO = db.PACKAGING_INFO.Find(id);
            if (pACKAGING_INFO == null)
            {
                return HttpNotFound();
            }
            return View(pACKAGING_INFO);
        }

        // POST: PACKAGING_INFO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PACKAGING_INFO pACKAGING_INFO = db.PACKAGING_INFO.Find(id);
            db.PACKAGING_INFO.Remove(pACKAGING_INFO);
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
