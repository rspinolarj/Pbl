using Pbl.Models;
using System.Web.Mvc;

namespace Pbl.Controllers
{
    public class HomeController : Controller
    {

        [Authorize(Roles = "Aluno,Diretor,Professor")]
        public ActionResult Index()
        {
            return View();
        }
    }
}