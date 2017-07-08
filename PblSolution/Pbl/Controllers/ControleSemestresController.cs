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
            TempData["Message"] = mSemestre.Add(semestre) ? "Semestre cadastrado com sucesso" : "Ação não realizada";
            return RedirectToAction("Create");
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult Update(int id)
        {
            SemestreViewModel novo = new SemestreViewModel();
            novo.idSemestre = id;
            return View(novo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Diretor")]
        public ActionResult Update(SemestreViewModel semestre)
        {
            MSemestre mSemestre = new MSemestre();
            TempData["Message"] = mSemestre.Update(semestre) ? "Semestre atualizado com sucesso" : "Ação não realizada";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult Delete(int idSemestre)
        {
            MSemestre mSemestre = new MSemestre();
            TempData["Message"] = mSemestre.Delete(mSemestre.BringOne(c => c.idSemestre == idSemestre)) ? "Semestre deletado com sucesso" : "Ação não realizada";
            return RedirectToAction("Index");
        }
    }
}