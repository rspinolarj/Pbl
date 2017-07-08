using Pbl.Models;
using System.Web.Mvc;

namespace Pbl.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Login","Login");
            }

            Usuario u = (Usuario)Session["Usuario"];
            return View();
        }
    }
}