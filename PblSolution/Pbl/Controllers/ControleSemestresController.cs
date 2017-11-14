using Pbl.Models;
using Pbl.Models.DbClasses;
using Pbl.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pbl.Controllers
{
    public class ControleSemestresController : Controller
    {
        // GET: ControleSemestres
        [Authorize(Roles = "Diretor")]
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            return View(new MSemestre().BringAll());
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult Create()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Diretor")]
        public ActionResult Create(SemestreViewModel semestre)
        {
            MSemestre mSemestre = new MSemestre();
            TempData["Message"] = mSemestre.Add(semestre) ? "Semestre cadastrado com sucesso" : "Açao nao realizada";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult Update(int id)
        {
            SemestreViewModel novo = new SemestreViewModel();
            Semestre semestre = new MSemestre().BringOne(c => c.idSemestre == id);
            novo.DataInicioModulo1 = semestre.Modulo.OrderBy(c => c.dtInicio).ElementAt(0).dtInicio.Value;
            novo.DataInicioModulo2 = semestre.Modulo.OrderBy(c => c.dtInicio).ElementAt(1).dtInicio.Value;
            novo.DataInicioModulo3 = semestre.Modulo.OrderBy(c => c.dtInicio).ElementAt(2).dtInicio.Value;
            novo.DataFinalModulo1 = semestre.Modulo.OrderBy(c => c.dtInicio).ElementAt(0).dtFim.Value;
            novo.DataFinalModulo2 = semestre.Modulo.OrderBy(c => c.dtInicio).ElementAt(1).dtFim.Value;
            novo.DataFinalModulo3 = semestre.Modulo.OrderBy(c => c.dtInicio).ElementAt(2).dtFim.Value;
            novo.idSemestre = id;
            novo.Descricao = semestre.descSemestre;
            return View(novo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Diretor")]
        public ActionResult Update(SemestreViewModel semestre)
        {
            MSemestre mSemestre = new MSemestre();
            TempData["Message"] = mSemestre.Update(semestre) ? "Semestre atualizado com sucesso" : "Açao nao realizada";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult Delete(int idSemestre)
        {
            MSemestre mSemestre = new MSemestre();
            TempData["Message"] = mSemestre.Delete(mSemestre.BringOne(c => c.idSemestre == idSemestre)) ? "Semestre deletado com sucesso" : "Açao nao realizada";
            return RedirectToAction("Index");
        }
    }
}