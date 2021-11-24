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


    }
}