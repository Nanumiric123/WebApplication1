using System.Web.Mvc;

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

        public ActionResult KANBAN_COLOR_FLOW()
        {
            return View();
        }

        public ActionResult BIN_ID_REGISTRATION()
        {
            return View();
        }

    }
}