using Pbl.Models;
using Pbl.Models.DbClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pbl.Controllers
{
    public class ControleNotasDisciplinasController : Controller
    {
        // GET: ControleNotasDisciplinas
        [Authorize(Roles = "Professor")]
        public ActionResult Index()
        {
            Usuario user = (Usuario)Session["Usuario"];
            Professor professor = new MProfessor().BringOne(c => c.idProfessor == user.idUsuario);
            List<Aula> aulasProfessor = new MAula().Bring(c => c.idProfessor == professor.idProfessor);
            return View(aulasProfessor);
        }

        
    }
}
