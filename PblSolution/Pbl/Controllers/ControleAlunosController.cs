using Pbl.Models;
using Pbl.Models.DbClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pbl.Controllers
{
    public class ControleAlunosController : Controller
    {
        // GET: ControleAlunos
        [Authorize(Roles = "Diretor")]
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            return View(new MAluno().BringAll());
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
        public ActionResult Create(string nomeAluno, string cpfAluno, string matriculaAluno)
        {
            Aluno aluno = new Aluno();
            aluno.nomeAluno = nomeAluno;
            aluno.cpfAluno = cpfAluno;
            aluno.matriculaAluno = matriculaAluno;
            aluno.ativo = true;
            MAluno mAluno = new MAluno();
            if (mAluno.Add(aluno))
            {
                Usuario novo = new Usuario();
                novo.login = aluno.cpfAluno;
                novo.senha = aluno.cpfAluno;
                novo.idTipoUsuario = 3;
                new MUsuario().Add(novo);
                TempData["Message"] = new MUsuarioAluno().Add(novo.idUsuario, aluno.idAluno) ? "Aluno cadastrado" : "Ação não foi realizada";
            }
            return RedirectToAction("Create");
        }
        public ActionResult Update(int id)
        {
            MAluno mAluno = new MAluno();
            return View(mAluno.BringOne(c => c.idAluno == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Diretor")]
        public ActionResult Update(int idAluno, string nomeAluno, string cpfAluno, string matriculaAluno)
        {
            Aluno aluno = new Aluno();
            aluno.idAluno = idAluno;
            aluno.nomeAluno = nomeAluno;
            aluno.cpfAluno = cpfAluno;
            aluno.matriculaAluno = matriculaAluno;
            if (new MAluno().Update(aluno))
            {
                TempData["Message"] = "Aluno atualizado com sucesso";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Ação não foi realizada";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult Delete(int id)
        {
            MAluno mAluno = new MAluno();
            Aluno aluno = mAluno.BringOne(c => c.idAluno == id);
            TempData["Message"] = mAluno.Delete(aluno) ? "Aluno deletado com sucesso" : "Ação não foi realizada";
            return RedirectToAction("Index", "ControleAlunos");
        }
    }
}