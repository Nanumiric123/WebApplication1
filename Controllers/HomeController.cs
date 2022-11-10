using PdfSharp.Pdf;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult KANBAN_CALCULATION()
        {
            return View();
        }

        public ActionResult SLIDER_INFORMATION()
        {
            return View();
        }

        public ActionResult KANBAN_COLOR_FLOW()
        {
            return View();
        }

        public ActionResult KANBAN_CARD_INFORMATION()
        {
            return View();
        }

        public ActionResult BIN_ID_REGISTRATION()
        {
            return View();
        }

        public ActionResult RECEIVING()
        {
            return View();
        }
        //
        public ActionResult RECEIVING_LABELLING()
        {
            return View();
        }
        public ActionResult PCB_LABELLING()
        {
            return View();
        }
        public ActionResult STOCK_OUT()
        {
            return View();
        }
        public ActionResult STOCK_IN()
        {
            return View();
        }
        public ActionResult KITTING()
        {
            return View();
        }
        public ActionResult SUPERMARKET()
        {
            return View();
        }
        public ActionResult DROP_IN()
        {
            return View();
        }
        public ActionResult PIECHART()
        {
            return View();
        }

        public ActionResult GENERATE_PICKINGLIST_WAREHOUSE()
        {

            return View();
        }


        public FileStreamResult GENERATE_PICKINGLIST_PDF_WAREHOUSE(string Text1, string Text2, string Text3, string Text4, string Text5,
            string Text6, string Text7, string Text8, string Text9, string Text10)
        {
            string PO1 = Text1;
            string PO2 = Text2;
            string PO3 = Text3;
            string PO4 = Text4;
            string PO5 = Text5;
            string PO6 = Text6;
            string PO7 = Text7;
            string PO8 = Text8;
            string PO9 = Text9;
            string P10 = Text10;

            string[] text = { PO1, PO2, PO3, PO4, PO5, PO6, PO7, PO8, PO9, P10 };

            PdfDocument document = new PdfDocument();



            if (text != null)
            {


                CreateKanbanPDF ckpdf = new CreateKanbanPDF();
                document = ckpdf.createWHpickingList(text);
                MemoryStream stream = new MemoryStream();
                document.Save(stream, false);

                return File(stream, "application/pdf");
            }
            else
            {
                MemoryStream stream = new MemoryStream();
                document.Save(stream, false);

                return File(stream, "application/pdf");
            }

        }

        public ActionResult Login(int? id)
        {
            ViewBag.denied = null;
            if (id == 1)
            {
                TempData["ID"] = "KANBAN";
            }
            else if (id == 2)
            {
                TempData["ID"] = "SUPERMARKET_SLIDER";
            }
            else
            {
                TempData["ID"] = "COLOR";
            }

            return View();
        }

        public ActionResult LoginError(int? id)
        {
            return View();
        }

        private IbsuinessContext ibctc = new IbsuinessContext();
        public ActionResult Check_authority_Login(string Text1, string Password1, int? id)
        {

            string username = Text1;
            string pass = Password1;
            string completeRoute = TempData["ID"].ToString();
            string role = null;

            var log_ingo = ibctc.LOGIN_INFO.Where(c => c.User == username && c.Password == pass).Select(x => x.Group).ToList();



            foreach (var i in log_ingo)
            {
                if (i.Contains("Administrator") || i.Contains("Kanban Master Group"))
                {
                    role = i;
                }
                else
                {
                    role = "others";
                }
            }

            if (role == "Administrator" || role == "Kanban Master Group")
            {
                if (completeRoute == "KANBAN")
                {
                    return RedirectToAction("Index", "KANBAN_MASTER");
                }
                else if (completeRoute == "SUPERMARKET_SLIDER")
                {
                    return RedirectToAction("sliderAddress", "KANBAN_MASTER");
                }
                else
                {
                    return RedirectToAction("Index", "SUPERMARKET_SLIDER");
                }

            }
            else if (role == "others")
            {
                if (completeRoute == "KANBAN")
                {
                    return RedirectToAction("Index", "KANBAN_MASTER", new { role = "supervisor" });
                }
                else if (completeRoute == "SUPERMARKET_SLIDER")
                {
                    return RedirectToAction("sliderAddress", "KANBAN_MASTER", new { role = "supervisor" });
                }
                else
                {
                    return RedirectToAction("Index", "SUPERMARKET_SLIDER", new { role = "supervisor" });
                }
            }
            else
            {
                return RedirectToAction("LoginError", "Home");
            }

        }

        [HttpGet]
        public FileStreamResult smtlabelpdf(string PN1, string PN2, string PN3, string PN4, string PN5, string PN6, string PN7,
                string RACK1, string RACK2, string RACK3, string RACK4, string RACK5, string RACK6, string RACK7)
        {
            List<RACK_MATERIAL> RL = new List<RACK_MATERIAL>();
            if (PN1.Length > 0 && RACK1.Length > 0 || PN1.Length > 0 || RACK1.Length > 0)
            {
                RACK_MATERIAL rm = new RACK_MATERIAL();
                rm.MATERIAL = PN1;
                rm.RACK = RACK1;
                RL.Add(rm);
            }
            if (PN2.Length > 0 && RACK2.Length > 0 || PN2.Length > 0 || RACK2.Length > 0)
            {
                RACK_MATERIAL rm = new RACK_MATERIAL();
                rm.MATERIAL = PN2;
                rm.RACK = RACK2;
                RL.Add(rm);
            }
            if (PN3.Length > 0 && RACK3.Length > 0 || PN3.Length > 0 || RACK3.Length > 0)
            {
                RACK_MATERIAL rm = new RACK_MATERIAL();
                rm.MATERIAL = PN3;
                rm.RACK = RACK3;
                RL.Add(rm);
            }
            if (PN4.Length > 0 && RACK4.Length > 0 || PN4.Length > 0 || RACK4.Length > 0)
            {
                RACK_MATERIAL rm = new RACK_MATERIAL();
                rm.MATERIAL = PN4;
                rm.RACK = RACK4;
                RL.Add(rm);
            }
            if (PN5.Length > 0 && RACK5.Length > 0 || PN5.Length > 0 || RACK5.Length > 0)
            {
                RACK_MATERIAL rm = new RACK_MATERIAL();
                rm.MATERIAL = PN5;
                rm.RACK = RACK5;
                RL.Add(rm);
            }
            if (PN6.Length > 0 && RACK6.Length > 0 || PN6.Length > 0 || RACK6.Length > 0)
            {
                RACK_MATERIAL rm = new RACK_MATERIAL();
                rm.MATERIAL = PN6;
                rm.RACK = RACK6;
                RL.Add(rm);
            }
            if (PN7.Length > 0 && RACK7.Length > 0 || PN7.Length > 0 || RACK7.Length > 0)
            {
                RACK_MATERIAL rm = new RACK_MATERIAL();
                rm.MATERIAL = PN7;
                rm.RACK = RACK7;
                RL.Add(rm);
            }


            CreateKanbanPDF CKPDF = new CreateKanbanPDF();
            PdfDocument document = CKPDF.create_smt_bin_label(RL);
            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            return File(stream, "application/pdf");
        }

        public ActionResult GenerateSMTStoreRackLabel()
        {
            return View();
        }

    }
}