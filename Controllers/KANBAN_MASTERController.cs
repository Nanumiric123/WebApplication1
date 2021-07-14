using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class KANBAN_MASTERController : Controller
    {
        private WebApplication1Context db = new WebApplication1Context();
        private sliderContext sliderdb = new sliderContext();


        // GET: KANBAN_MASTER
        public ActionResult Index(string SearchPart, string SearchModel, string SearchBin, string SearchPartName, string SearchLine, string SearchbinType, string SearchLoc, string SearchSliderAddress,
            string SearchRemarks, string SearchSupplier, string SearchProcess, string SearchKittingSlider)
        {
            var kanbanMaster = from s in db.KANBAN_MASTER.Select(p => new KANBAN_MASTER_index
            {
                ID = p.ID,
                PART_NO = p.PART_NO,
                PART_NAME = p.PART_NAME,
                MODEL = p.MODEL,
                LINE = p.LINE,
                OUTPUT = p.OUTPUT,
                USAGE = p.USAGE,
                PROCESS = p.PROCESS,
                QTY_PER_TRAY = p.QTY_PER_TRAY,
                TARY_PER_BIN = p.TARY_PER_BIN,
                QTY_PER_BIN = p.QTY_PER_BIN,
                BIN_TYPE = p.BIN_TYPE,
                LOCATION = p.LOCATION,
                SLIDER_ADDRESS = p.SLIDER_ADDRESS,
                KITTING_SLIDER = p.KITTING_SLIDER,
                STORE_KANBAN_QTY = p.STORE_KANBAN_QTY,
                PROD_KANBAN_QTY = p.PROD_KANBAN_QTY,
                BASIC_FINISH_DATE = p.BASIC_FINISH_DATE,
                REVISION = p.REVISION,
                BIN_NUMBER_END = p.BIN_NUMBER_END,
                CYCLE_TIME = p.CYCLE_TIME,
                REMARKS = p.REMARKS,
                SUPPLIER = p.SUPPLIER
            })
                               select s;

            if (!String.IsNullOrEmpty(SearchKittingSlider))
            {
                kanbanMaster = kanbanMaster.Where(s => s.KITTING_SLIDER.Contains(SearchKittingSlider));

            }

            if (!String.IsNullOrEmpty(SearchProcess))
            {
                kanbanMaster = kanbanMaster.Where(s => s.PROCESS.Contains(SearchProcess));

            }

            if (!String.IsNullOrEmpty(SearchSupplier))
            {
                kanbanMaster = kanbanMaster.Where(s => s.SUPPLIER.Contains(SearchSupplier));

            }
            if (!String.IsNullOrEmpty(SearchRemarks))
            {
                kanbanMaster = kanbanMaster.Where(s => s.REMARKS.Contains(SearchRemarks));

            }
            if (!String.IsNullOrEmpty(SearchSliderAddress))
            {
                kanbanMaster = kanbanMaster.Where(s => s.SLIDER_ADDRESS.Contains(SearchSliderAddress));

            }
            if (!String.IsNullOrEmpty(SearchLoc))
            {
                kanbanMaster = kanbanMaster.Where(s => s.LOCATION.Contains(SearchLoc));

            }
            if (!String.IsNullOrEmpty(SearchbinType))
            {
                kanbanMaster = kanbanMaster.Where(s => s.BIN_TYPE.Contains(SearchbinType));

            }
            if (!String.IsNullOrEmpty(SearchPart))
            {
                kanbanMaster = kanbanMaster.Where(s => s.PART_NO.Contains(SearchPart));

            }
            if (!String.IsNullOrEmpty(SearchModel))
            {
                kanbanMaster = kanbanMaster.Where(s => s.MODEL.Contains(SearchModel));

            }

            if (!String.IsNullOrEmpty(SearchBin))
            {
                kanbanMaster = kanbanMaster.Where(s => s.BIN_NUMBER_END.Contains(SearchBin));

            }

            if (!String.IsNullOrEmpty(SearchPartName))
            {
                kanbanMaster = kanbanMaster.Where(s => s.PART_NAME.Contains(SearchPartName));

            }

            if (!String.IsNullOrEmpty(SearchLine))
            {
                kanbanMaster = kanbanMaster.Where(s => s.LINE.Contains(SearchLine));

            }

            return View(kanbanMaster.OrderBy(s => s.ID).ToList());
        }

        // GET: KANBAN_MASTER/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KANBAN_MASTER kANBAN_MASTER = db.KANBAN_MASTER.Find(id);

            if (kANBAN_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(kANBAN_MASTER);
        }

            // GET : SliderAddress
            public ActionResult sliderAddress(string ddlRacks)
            {
                var RACKS_LISTS = from rk in sliderdb.SUPERMARKET_SLIDER select rk;
                var racks_only = RACKS_LISTS.Select(x => x.RACK).Distinct().ToList();

                ViewBag.rack_lists = racks_only.OrderBy(rk => rk).ToList();
                ViewBag.Rack_name = ddlRacks;

                    if (!String.IsNullOrEmpty(ddlRacks))
                    {
                        IEnumerable<WebApplication1.Models.SUPERMARKET_SLIDER_JOINED> _sliderhoinesd = GeSlider_jpined().ToList();

                        _sliderhoinesd = _sliderhoinesd.OrderBy(x => x.SLIDER_ADDRESS).Where(s => s.RACK == ddlRacks).ToList();
                        return View(_sliderhoinesd);
                    }
                    else
                    {
                        IEnumerable<WebApplication1.Models.SUPERMARKET_SLIDER_JOINED> _sliderhoinesd = GeSlider_jpined().ToList();

                        _sliderhoinesd = _sliderhoinesd.Where(s => s.RACK == "Rack 1A").ToList();
                        return View(_sliderhoinesd);
                    }

           
            }

        public IEnumerable<SUPERMARKET_SLIDER_JOINED> GeSlider_jpined()
        {
            var kanbanMaster = from s in db.KANBAN_MASTER.Select(p => new KANBAN_MASTER_index
            {
                ID = p.ID,
                PART_NO = p.PART_NO,
                PART_NAME = p.PART_NAME,
                MODEL = p.MODEL,
                LINE = p.LINE,
                OUTPUT = p.OUTPUT,
                USAGE = p.USAGE,
                PROCESS = p.PROCESS,
                QTY_PER_TRAY = p.QTY_PER_TRAY,
                TARY_PER_BIN = p.TARY_PER_BIN,
                QTY_PER_BIN = p.QTY_PER_BIN,
                BIN_TYPE = p.BIN_TYPE,
                LOCATION = p.LOCATION,
                SLIDER_ADDRESS = p.SLIDER_ADDRESS,
                KITTING_SLIDER = p.KITTING_SLIDER,
                STORE_KANBAN_QTY = p.STORE_KANBAN_QTY,
                PROD_KANBAN_QTY = p.PROD_KANBAN_QTY,
                BASIC_FINISH_DATE = p.BASIC_FINISH_DATE,
                REVISION = p.REVISION,
                BIN_NUMBER_END = p.BIN_NUMBER_END,
                CYCLE_TIME = p.CYCLE_TIME,
                REMARKS = p.REMARKS,
                SUPPLIER = p.SUPPLIER
            })
                               select s;
            var Kanbanmasterlist = kanbanMaster.ToList();
            var sliderAddresses = sliderdb.SUPERMARKET_SLIDER.ToList();
           /*

            var joinkanbansupermarket = from sadb in sliderAddresses
                                        join kmsl in Kanbanmasterlist on
                                        sadb.PART_NUMBER equals kmsl.PART_NO into temp
                                        from kmsl in temp.DefaultIfEmpty()
                                        select new
                                        {
                                            ID = sadb.ID,
                                            SLIDER_ADDRESS = sadb.SLIDER_ADDRESS,
                                            PART_NUMBER = sadb.PART_NUMBER,
                                            STATUS = sadb.STATUS,
                                            RACK = sadb.RACK,
                                            AREA = sadb.AREA,
                                            BIN = sadb.BIN,
                                            PROCESS = kmsl == null ? String.Empty : kmsl.PROCESS,
                                            COLOR = sadb.COLOR
                                        };

            var join2 = joinkanbansupermarket.ToList();

            List<SUPERMARKET_SLIDER_JOINED> join3 = new List<SUPERMARKET_SLIDER_JOINED>();
            */
            List<SUPERMARKET_SLIDER_JOINED> new_join = new List<SUPERMARKET_SLIDER_JOINED>();


                foreach(var k in sliderAddresses)
                {
                   int found_count = 0;
                    foreach (var j in Kanbanmasterlist)
                    {
                        if (k.PART_NUMBER.Contains(j.PART_NO))
                        {
                            SUPERMARKET_SLIDER_JOINED l = new SUPERMARKET_SLIDER_JOINED();
                            l.ID = k.ID;
                            l.PART_NUMBER = k.PART_NUMBER;
                            l.RACK = k.RACK;
                            l.SLIDER_ADDRESS = k.SLIDER_ADDRESS;
                            l.STATUS = k.STATUS;
                            l.AREA = k.AREA;
                            l.BIN = k.BIN;
                            l.PROCESS = j.PROCESS;
                            l.COLOR = k.COLOR;
                            new_join.Add(l);
                            found_count++;

                        }
                        else
                        {

                        }

                    }
                    if(found_count == 0)
                    {
                        SUPERMARKET_SLIDER_JOINED l = new SUPERMARKET_SLIDER_JOINED();
                        l.ID = k.ID;
                        l.PART_NUMBER = k.PART_NUMBER;
                        l.RACK = k.RACK;
                        l.SLIDER_ADDRESS = k.SLIDER_ADDRESS;
                        l.STATUS = k.STATUS;
                        l.AREA = k.AREA;
                        l.BIN = k.BIN;
                        l.PROCESS = string.Empty;
                        l.COLOR = k.COLOR;
                        new_join.Add(l);
                    }

                }

                
                /*
            foreach (var i in join2)
            {
                SUPERMARKET_SLIDER_JOINED K = new SUPERMARKET_SLIDER_JOINED();
                K.ID = i.ID;
                K.PART_NUMBER = i.PART_NUMBER;
                K.RACK = i.RACK;
                K.SLIDER_ADDRESS = i.SLIDER_ADDRESS;
                K.STATUS = i.STATUS;
                K.AREA = i.AREA;
                K.BIN = i.BIN;
                K.PROCESS = i.PROCESS;
                K.COLOR = i.COLOR;

                join3.Add(K);


            }
                */

            //return join3.OrderBy(x => x.SLIDER_ADDRESS);
            //return join3.OrderByDescending(x => x.SLIDER_ADDRESS);
            return new_join.OrderByDescending(x => x.SLIDER_ADDRESS);
        }

        // GET: KANBAN_MASTER/Create
        public ActionResult Create()
        {
            return View();
        }

        public byte[] Stream2Bytes(Stream stream, int chunkSize = 1024)
        {
            if (stream == null)
            {
                throw new System.ArgumentException("Parameter cannot be null", "stream");
            }

            if (chunkSize < 1)
            {
                throw new System.ArgumentException("Parameter must be greater than zero", "chunkSize");
            }

            if (chunkSize > 1024 * 64)
            {
                throw new System.ArgumentException(String.Format("Parameter must be less or equal {0}", 1024 * 64), "chunkSize");
            }

            List<byte> buffers = new List<byte>();

            using (BinaryReader br = new BinaryReader(stream))
            {
                byte[] chunk = br.ReadBytes(chunkSize);

                while (chunk.Length > 0)
                {
                    buffers.AddRange(chunk);
                    chunk = br.ReadBytes(chunkSize);
                }
            }
            return buffers.ToArray();

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

            result = int.Parse(Math.Floor(SUPERMARKET_STOCK_90_MIN).ToString());
            result = result + 5;

            return result;
        }
        // POST: KANBAN_MASTER/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PART_NO,PART_NAME,MODEL,LINE,OUTPUT,USAGE,PROCESS,QTY_PER_TRAY,TARY_PER_BIN,QTY_PER_BIN,BIN_TYPE,LOCATION,SLIDER_ADDRESS,KITTING_SLIDER,STORE_KANBAN_QTY,PROD_KANBAN_QTY,BASIC_FINISH_DATE,REVISION,BIN_NUMBER_END,SUPPLIER,REMARKS,CYCLE_TIME")] KANBAN_MASTER kANBAN_MASTER, HttpPostedFileBase file)
        {
            if (file != null)
            {
                MemoryStream target = new MemoryStream();
                file.InputStream.CopyTo(target);
                byte[] data = target.ToArray();
                kANBAN_MASTER.PHOTO = data;
            }
            else
            {
                kANBAN_MASTER.PHOTO = null;
            }

            string[] lines_output = kANBAN_MASTER.LINE.ToString().Split();


            decimal line_output_decimal = lines_output.Length;
            decimal STORE_OUTPUT_DECIMAL = decimal.Parse(kANBAN_MASTER.OUTPUT.ToString());
            decimal bin_qty_decimal = decimal.Parse(kANBAN_MASTER.QTY_PER_BIN.ToString());
            decimal per_usage_decimal = decimal.Parse(kANBAN_MASTER.USAGE.ToString());


            kANBAN_MASTER.PROD_KANBAN_QTY = calculate_prod_kanban_qty(line_output_decimal,
                bin_qty_decimal, per_usage_decimal);

            kANBAN_MASTER.STORE_KANBAN_QTY = calculate_store_kanban_qty(STORE_OUTPUT_DECIMAL,
                bin_qty_decimal, per_usage_decimal);

            string BIN_NUMBER = kANBAN_MASTER.BIN_NUMBER_END;

            SUPERMARKET_SLIDER slider_address_data = new SUPERMARKET_SLIDER();



            int bin_num = 0;
            try
            {
                string[] bin_split = BIN_NUMBER.Split('S');
                bin_num = int.Parse(bin_split[1]);
                bin_num = bin_num + kANBAN_MASTER.STORE_KANBAN_QTY;
                kANBAN_MASTER.BIN_NUMBER_END = "BS0" + bin_num;
            }
            catch { kANBAN_MASTER.BIN_NUMBER_END = ""; }


            if (ModelState.IsValid)
            {
                db.KANBAN_MASTER.Add(kANBAN_MASTER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kANBAN_MASTER);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SliderAddressEdit([Bind(Include = "ID,PHOTO,PART_NO,PART_NAME,MODEL,LINE,OUTPUT,USAGE,PROCESS,QTY_PER_TRAY,TARY_PER_BIN,QTY_PER_BIN,BIN_TYPE,LOCATION,SLIDER_ADDRESS,KITTING_SLIDER,STORE_KANBAN_QTY,PROD_KANBAN_QTY,BASIC_FINISH_DATE,REVISION,BIN_NUMBER_END,REMARKS,SUPPLIER,CYCLE_TIME")] KANBAN_MASTER kANBAN_MASTER, HttpPostedFileBase file)
        {
            if (file != null)
            {
                MemoryStream target = new MemoryStream();
                file.InputStream.CopyTo(target);
                byte[] data = target.ToArray();
                kANBAN_MASTER.PHOTO = data;
            }
            else
            {
                if (TempData["TempModel"] != null)
                {
                    byte[] photo_temp = (byte[])TempData["TempModel"];
                    kANBAN_MASTER.PHOTO = photo_temp;
                }
                else
                {
                    byte[] photo_temp = (byte[])Session["photo"];
                    kANBAN_MASTER.PHOTO = photo_temp;
                }

            }
            string[] lines_output = kANBAN_MASTER.LINE.ToString().Split();


            decimal line_output_decimal = lines_output.Length;
            decimal lineoutput_decimal_store = decimal.Parse(kANBAN_MASTER.OUTPUT.ToString());

            decimal bin_qty_decimal = decimal.Parse(kANBAN_MASTER.QTY_PER_BIN.ToString());
            decimal per_usage_decimal = decimal.Parse(kANBAN_MASTER.USAGE.ToString());

            kANBAN_MASTER.PROD_KANBAN_QTY = calculate_prod_kanban_qty(line_output_decimal,
                bin_qty_decimal, per_usage_decimal);

            kANBAN_MASTER.STORE_KANBAN_QTY = calculate_store_kanban_qty(lineoutput_decimal_store,
                bin_qty_decimal, per_usage_decimal);

            string BIN_NUMBER = kANBAN_MASTER.BIN_NUMBER_END;

            int bin_num = 0;
            try
            {
                string[] bin_split = BIN_NUMBER.Split('S');
                bin_num = int.Parse(bin_split[1]);
                bin_num = bin_num + kANBAN_MASTER.STORE_KANBAN_QTY;
                kANBAN_MASTER.BIN_NUMBER_END = "BS0" + bin_num;
            }
            catch { kANBAN_MASTER.BIN_NUMBER_END = ""; }





            if (ModelState.IsValid)
            {
                db.Entry(kANBAN_MASTER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = kANBAN_MASTER.ID });
            }
            return View(kANBAN_MASTER);
        }

        // GET: KANBAN_MASTER/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KANBAN_MASTER kANBAN_MASTER = db.KANBAN_MASTER.Find(id);
            TempData["TempModel"] = kANBAN_MASTER.PHOTO;
            Session["photo"] = kANBAN_MASTER.PHOTO;

            if (kANBAN_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(kANBAN_MASTER);
        }

        // GET: KANBAN_MASTER/Details/5
        public ActionResult SliderAddressDetails(string sliderAddress)
        {
            if (sliderAddress == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var kANBAN_MASTER2 = (from s in db.KANBAN_MASTER where s.SLIDER_ADDRESS.Contains(sliderAddress) select s).ToList();

            KANBAN_MASTER kANBAN_MASTER = new KANBAN_MASTER();

            foreach (var a in kANBAN_MASTER2)
            {
                kANBAN_MASTER.BASIC_FINISH_DATE = a.BASIC_FINISH_DATE;
                kANBAN_MASTER.BIN_NUMBER_END = a.BIN_NUMBER_END;
                kANBAN_MASTER.BIN_TYPE = a.BIN_TYPE;
                kANBAN_MASTER.CYCLE_TIME = a.CYCLE_TIME;
                kANBAN_MASTER.ID = a.ID;
                kANBAN_MASTER.KITTING_SLIDER = a.KITTING_SLIDER;
                kANBAN_MASTER.LINE = a.LINE;
                kANBAN_MASTER.LOCATION = a.LOCATION;
                kANBAN_MASTER.MODEL = a.MODEL;
                kANBAN_MASTER.OUTPUT = a.OUTPUT;
                kANBAN_MASTER.PART_NAME = a.PART_NAME;
                kANBAN_MASTER.PART_NO = a.PART_NO;
                kANBAN_MASTER.PHOTO = a.PHOTO;
                kANBAN_MASTER.PROCESS = a.PROCESS;
                kANBAN_MASTER.PROD_KANBAN_QTY = a.PROD_KANBAN_QTY;
                kANBAN_MASTER.QTY_PER_BIN = a.QTY_PER_BIN;
                kANBAN_MASTER.QTY_PER_TRAY = a.QTY_PER_TRAY;
                kANBAN_MASTER.REMARKS = a.REMARKS;
                kANBAN_MASTER.REVISION = a.REVISION;
                kANBAN_MASTER.SLIDER_ADDRESS = a.SLIDER_ADDRESS;
                kANBAN_MASTER.STORE_KANBAN_QTY = a.STORE_KANBAN_QTY;
                kANBAN_MASTER.SUPPLIER = a.SUPPLIER;
                kANBAN_MASTER.TARY_PER_BIN = a.TARY_PER_BIN;
                kANBAN_MASTER.USAGE = a.USAGE;

            }
            TempData["TempModel"] = kANBAN_MASTER.PHOTO;
            Session["photo"] = kANBAN_MASTER.PHOTO;
            if (kANBAN_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(kANBAN_MASTER);
        }
        // POST: KANBAN_MASTER/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PHOTO,PART_NO,PART_NAME,MODEL,LINE,OUTPUT,USAGE,PROCESS,QTY_PER_TRAY,TARY_PER_BIN,QTY_PER_BIN,BIN_TYPE,LOCATION,SLIDER_ADDRESS,KITTING_SLIDER,STORE_KANBAN_QTY,PROD_KANBAN_QTY,BASIC_FINISH_DATE,REVISION,BIN_NUMBER_END,REMARKS,SUPPLIER,CYCLE_TIME")] KANBAN_MASTER kANBAN_MASTER, HttpPostedFileBase file)
        {
            if (file != null)
            {
                MemoryStream target = new MemoryStream();
                file.InputStream.CopyTo(target);
                byte[] data = target.ToArray();
                kANBAN_MASTER.PHOTO = data;
            }
            else
            {
                if (TempData["TempModel"] != null)
                {
                    byte[] photo_temp = (byte[])TempData["TempModel"];
                    kANBAN_MASTER.PHOTO = photo_temp;
                }
                else
                {
                    byte[] photo_temp = (byte[])Session["photo"];
                    kANBAN_MASTER.PHOTO = photo_temp;
                }

            }
            string[] lines_output = kANBAN_MASTER.LINE.ToString().Split();


            decimal line_output_decimal = lines_output.Length;
            decimal bin_qty_decimal = decimal.Parse(kANBAN_MASTER.QTY_PER_BIN.ToString());
            decimal per_usage_decimal = decimal.Parse(kANBAN_MASTER.USAGE.ToString());
            decimal lineoutput_decimal_store = decimal.Parse(kANBAN_MASTER.OUTPUT.ToString());

            kANBAN_MASTER.PROD_KANBAN_QTY = calculate_prod_kanban_qty(line_output_decimal,
                bin_qty_decimal, per_usage_decimal);

            kANBAN_MASTER.STORE_KANBAN_QTY = calculate_store_kanban_qty(lineoutput_decimal_store,
                bin_qty_decimal, per_usage_decimal);

            string BIN_NUMBER = kANBAN_MASTER.BIN_NUMBER_END;

            int bin_num = 0;
            try
            {
                string[] bin_split = BIN_NUMBER.Split('S');
                bin_num = int.Parse(bin_split[1]);
                bin_num = bin_num + kANBAN_MASTER.STORE_KANBAN_QTY;
                kANBAN_MASTER.BIN_NUMBER_END = "BS0" + bin_num;
            }
            catch { kANBAN_MASTER.BIN_NUMBER_END = ""; }





            if (ModelState.IsValid)
            {
                db.Entry(kANBAN_MASTER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = kANBAN_MASTER.ID });
            }
            return View(kANBAN_MASTER);
        }





        // GET: KANBAN_MASTER/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KANBAN_MASTER kANBAN_MASTER = db.KANBAN_MASTER.Find(id);
            if (kANBAN_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(kANBAN_MASTER);
        }

        // POST: KANBAN_MASTER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KANBAN_MASTER kANBAN_MASTER = db.KANBAN_MASTER.Find(id);
            db.KANBAN_MASTER.Remove(kANBAN_MASTER);
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


        //GET : KANBAN_MASTER/CreateProdKanban
        public ActionResult CreateProdKanban(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            KANBAN_MASTER kANBAN_MASTER = db.KANBAN_MASTER.Find(id);


            if (kANBAN_MASTER == null)
            {
                return HttpNotFound();
            }

            return View(kANBAN_MASTER);
        }

        [HttpGet]
        public FileStreamResult CreateProdKanbanPdf(int? id, [Bind(Include = "ID,PHOTO,PART_NO,PART_NAME,MODEL,LINE,OUTPUT,USAGE,PROCESS,QTY_PER_TRAY,TARY_PER_BIN,QTY_PER_BIN,BIN_TYPE,LOCATION,SLIDER_ADDRESS,KITTING_SLIDER,STORE_KANBAN_QTY,PROD_KANBAN_QTY,BASIC_FINISH_DATE,REVISION,CYCLE_TIME")] KANBAN_MASTER kANBAN_MASTER, string kanban_color)
        {
            KANBAN_MASTER kANBAN_MASTER_db = db.KANBAN_MASTER.Find(kANBAN_MASTER.ID);
            kANBAN_MASTER_db.LINE = kANBAN_MASTER.LINE;
            if (kanban_color == "FG")
            {
                CreateKanbanPDF CKPDF = new CreateKanbanPDF();
                PdfDocument document = CKPDF.create_FG_kanban(kANBAN_MASTER_db);
                MemoryStream stream = new MemoryStream();
                document.Save(stream, false);
                return File(stream, "application/pdf");
            }
            else
            {
                CreateKanbanPDF CKPDF = new CreateKanbanPDF();
                PdfDocument document = CKPDF.create_Production_kanban(kANBAN_MASTER_db, kanban_color);
                MemoryStream stream = new MemoryStream();
                document.Save(stream, false);
                return File(stream, "application/pdf");
            }

        }

        [HttpGet]
        public ActionResult update_slider()
        {


            var kanbanMaster = from s in db.KANBAN_MASTER.Select(p => new KANBAN_MASTER_index
            {
                ID = p.ID,
                PART_NO = p.PART_NO,
                PART_NAME = p.PART_NAME,
                MODEL = p.MODEL,
                LINE = p.LINE,
                OUTPUT = p.OUTPUT,
                USAGE = p.USAGE,
                PROCESS = p.PROCESS,
                QTY_PER_TRAY = p.QTY_PER_TRAY,
                TARY_PER_BIN = p.TARY_PER_BIN,
                QTY_PER_BIN = p.QTY_PER_BIN,
                BIN_TYPE = p.BIN_TYPE,
                LOCATION = p.LOCATION,
                SLIDER_ADDRESS = p.SLIDER_ADDRESS,
                KITTING_SLIDER = p.KITTING_SLIDER,
                STORE_KANBAN_QTY = p.STORE_KANBAN_QTY,
                PROD_KANBAN_QTY = p.PROD_KANBAN_QTY,
                BASIC_FINISH_DATE = p.BASIC_FINISH_DATE,
                REVISION = p.REVISION,
                BIN_NUMBER_END = p.BIN_NUMBER_END,
                CYCLE_TIME = p.CYCLE_TIME,
                REMARKS = p.REMARKS,
                SUPPLIER = p.SUPPLIER
            })
                               select s;

            var sUPERMARKET_SLIDER = sliderdb.SUPERMARKET_SLIDER.ToList();
            SUPERMARKET_SLIDER smk_upd = new SUPERMARKET_SLIDER();



            foreach (var m in sUPERMARKET_SLIDER)
            {
                int p1 = 0;
                foreach (var k in kanbanMaster)
                {
                    DateTime basic_fin_date = Convert.ToDateTime(k.BASIC_FINISH_DATE);
                    DateTime date_tdy = DateTime.Now;

                    TimeSpan value_betweet = basic_fin_date.Subtract(date_tdy);

                    int days_span = value_betweet.Days;

                    if (!(k.SLIDER_ADDRESS is null))
                    {
                        if (k.SLIDER_ADDRESS.Contains(m.SLIDER_ADDRESS))
                        {

                            if (m.PART_NUMBER != "EMPTY" && m.MULTIPLE_PLART == true)
                            {
                                string part_num = m.PART_NUMBER + " " + k.PART_NO;
                                smk_upd.PART_NUMBER = part_num;
                            }
                            else
                            {
                                smk_upd.PART_NUMBER = k.PART_NO;
                            }
                            //smk_upd.PART_NUMBER = k.PART_NO;
                            smk_upd.ID = m.ID;
                            smk_upd.RACK = m.RACK;
                            smk_upd.SLIDER_ADDRESS = m.SLIDER_ADDRESS;
                            smk_upd.MULTIPLE_PLART = m.MULTIPLE_PLART;
                            if (days_span <= 3)
                            {
                                smk_upd.STATUS = "SLOW MOVING";
                            }
                            else if (days_span >= 3)
                            {
                                smk_upd.STATUS = "MOVING";
                            }
                            else if (days_span < 0)
                            {
                                smk_upd.STATUS = "EOL";
                            }
                            
                            smk_upd.BIN = m.BIN;
                            smk_upd.AREA = m.AREA;
                            smk_upd.COLOR = m.COLOR;
                            update_rows_slider_kanban(smk_upd);
                            p1++;
                        }
                    }
                    
                }
                if(p1 == 0)
                {
                    smk_upd.PART_NUMBER = "EMPTY";
                    smk_upd.ID = m.ID;
                    smk_upd.RACK = m.RACK;
                    smk_upd.SLIDER_ADDRESS = m.SLIDER_ADDRESS;
                    smk_upd.STATUS = "EMPTY";
                    smk_upd.BIN = m.BIN;
                    smk_upd.AREA = m.AREA;
                    smk_upd.COLOR = m.COLOR;
                    update_rows_slider_kanban(smk_upd);
                }
            }
            return RedirectToAction("sliderAddress");
        }

        [HttpGet]
        public FileStreamResult CreateStoreKanbanPdf(int? id)
        {
            KANBAN_MASTER kANBAN_MASTER = db.KANBAN_MASTER.Find(id);
            if (kANBAN_MASTER.PROCESS == "H0-SB" || kANBAN_MASTER.PROCESS.Contains("H0-SB"))
            {
                CreateKanbanPDF CKPDF = new CreateKanbanPDF();
                PdfDocument document = CKPDF.create_Store_kanban_Large(kANBAN_MASTER);
                MemoryStream stream = new MemoryStream();
                document.Save(stream, false);
                return File(stream, "application/pdf");
            }
            else
            {
                CreateKanbanPDF CKPDF = new CreateKanbanPDF();
                PdfDocument document = CKPDF.create_Store_kanban(kANBAN_MASTER);
                MemoryStream stream = new MemoryStream();
                document.Save(stream, false);
                return File(stream, "application/pdf");
            }

        }

        public ActionResult CreateSliderKanban()
        {
            return View();
        }

        [HttpGet]
        public FileStreamResult CreateSLIDERKanbanPdf(string PN1, string PN2, string PN3, string PN4, string PN5, string PN6, string PN7, string PN8, string PN9)
        {
            int lenpn1 = PN1.Length;
            int lenpn2 = PN2.Length;
            int lenpn3 = PN3.Length;
            int lenpn4 = PN4.Length;
            int lenpn5 = PN5.Length;
            int lenpn6 = PN6.Length;
            int lenpn7 = PN7.Length;
            int lenpn8 = PN8.Length;
            int lenpn9 = PN9.Length;

            List<string> part_numbers = new List<string>();
            CreateKanbanPDF CKPDF = new CreateKanbanPDF();
            PdfDocument document = new PdfDocument();

            if (lenpn1 != 0)
            {
                part_numbers.Add(PN1);
            }

            if (lenpn2 != 0)
            {
                part_numbers.Add(PN2);
            }
            if (lenpn3 != 0)
            {
                part_numbers.Add(PN3);
            }

            if (lenpn4 != 0)
            {
                part_numbers.Add(PN4);
            }

            if (lenpn5 != 0)
            {
                part_numbers.Add(PN5);
            }

            if (lenpn6 != 0)
            {
                part_numbers.Add(PN6);
            }
            if (lenpn7 != 0)
            {
                part_numbers.Add(PN7);
            }
            if (lenpn8 != 0)
            {
                part_numbers.Add(PN8);
            }
            if (lenpn9 != 0)
            {
                part_numbers.Add(PN9);
            }

            int i = 1;
            List<string> empty_part_num = new List<string>();
            List<string> not_empty_part_num = new List<string>();
            IEnumerable<WebApplication1.Models.KANBAN_MASTER> kANBAN_MASTER = new List<KANBAN_MASTER>();

            foreach (var prts in part_numbers)
            {


                if (prts.Contains('$') == true)
                {

                    empty_part_num.Add(prts);
                }
                else
                {

                    not_empty_part_num.Add(prts);

                }

            }

            if (part_numbers.Count > 0)
            {
                kANBAN_MASTER = db.KANBAN_MASTER.Where(x => not_empty_part_num.Contains(x.PART_NO));

            }

            document = CKPDF.create_slider_kanban(kANBAN_MASTER.ToList(), empty_part_num);

            //PdfDocument document = new PdfDocument();
            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            return File(stream, "application/pdf");
        }

        private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
        {
            return (System.Drawing.Image)(new Bitmap(imgToResize, size));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        private void update_rows_slider_kanban(SUPERMARKET_SLIDER sUPER_SLIDER)
        {
            if (ModelState.IsValid)
            {
                var existingEntity = sliderdb.SUPERMARKET_SLIDER.Find(sUPER_SLIDER.ID);

                //sliderdb.Entry(sUPER_SLIDER).State = EntityState.Modified;
                sliderdb.Entry(existingEntity).CurrentValues.SetValues(sUPER_SLIDER);
                sliderdb.SaveChanges();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        private void update_rows_slider_kanban_create_kanban(SUPERMARKET_SLIDER sUPER_SLIDER)
        {
            if (ModelState.IsValid)
            {
                var existingEntity = sliderdb.SUPERMARKET_SLIDER.Find(sUPER_SLIDER.ID);

                //sliderdb.Entry(sUPER_SLIDER).State = EntityState.Modified;
                sliderdb.Entry(existingEntity).CurrentValues.SetValues(sUPER_SLIDER);
                sliderdb.SaveChanges();
            }

        }


    }
}
