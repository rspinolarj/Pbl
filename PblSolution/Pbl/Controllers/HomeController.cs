using Pbl.Models;
using System.Web.Mvc;

namespace Pbl.Controllers
{
    public class HomeController : Controller
    {

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}