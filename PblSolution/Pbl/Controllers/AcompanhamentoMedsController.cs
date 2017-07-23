using Pbl.Models;
using Pbl.Models.DbClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pbl.Controllers
{
    public class AcompanhamentoMedsController : Controller
    {
        // GET: AcompanhamentoMeds
        [Authorize(Roles = "Aluno")]
        public ActionResult Index()
        {
            int idUsuario = Convert.ToInt32(HttpContext.User.Identity.Name);
            Usuario user = new MUsuario().BringOne(c => c.idUsuario == idUsuario);
            Aluno aluno = user.Aluno.FirstOrDefault();
            return View();
        }
    }
}