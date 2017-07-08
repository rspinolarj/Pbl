using Pbl.Models;
using Pbl.Models.DbClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pbl.Controllers
{
    public class ControleTipoDisciplinaController : Controller
    {
        // GET: ControleTipoDisciplina
        [Authorize(Roles = "Diretor")]
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            return View(new MTipoDisciplina().BringAll());
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
        public ActionResult Create(TipoDisciplina TipoDisciplina)
        {
            MTipoDisciplina mTipoDisciplina = new MTipoDisciplina();
            TempData["Message"] = mTipoDisciplina.Add(TipoDisciplina) ? "Tipo de disciplina cadastrado" : "Ação não foi realizada";
            return RedirectToAction("Create");
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult Update(int id)
        {
            MTipoDisciplina mTipoDisciplina = new MTipoDisciplina();
            return View(mTipoDisciplina.BringOne(c => c.idTipoDisciplina == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Diretor")]
        public ActionResult Update(TipoDisciplina TipoDisciplina)
        {
            TempData["Message"] = new MTipoDisciplina().Update(TipoDisciplina) ? "Tipo de disciplina atualizado com sucesso" : "Ação não foi realizada";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult Delete(int id)
        {
            MTipoDisciplina mTipoDisciplina = new MTipoDisciplina();
            TipoDisciplina TipoDisciplina = mTipoDisciplina.BringOne(c => c.idTipoDisciplina == id);
            TempData["Message"] = mTipoDisciplina.Delete(TipoDisciplina) ? "Tipo de disciplina deletado com sucesso" : "Ação não foi realizada";
            return RedirectToAction("Index");
        }
    }
}