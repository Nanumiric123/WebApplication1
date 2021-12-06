using System.Web.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;


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

        public ActionResult Login(int? id)
        {
            ViewBag.denied = null;
            if (id == 1)
            {
                TempData["ID"] = "KANBAN";
            }
            else if (id ==2)
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
        public ActionResult Check_authority_Login(string Text1,string Password1,int? id)
        {

            string username = Text1;
            string pass = Password1;
            string completeRoute = TempData["ID"].ToString();
            string role = null;

            var log_ingo = ibctc.LOGIN_INFO.Where(c => c.User == username && c.Password == pass).Select(x => x.Group).ToList();
            


            foreach(var i in log_ingo)
            {
                if (i.Contains("Administrator") || i.Contains("Kanban Master Group"))
                {
                    role = i;
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
            else
            {
                return RedirectToAction("LoginError", "Home");
            }

        }
    }
}