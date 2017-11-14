using Pbl.Models;
using Pbl.Models.DbClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pbl.Controllers
{
    public class ControleProfessoresController : Controller
    {
        // GET: ControleProfessores
        [Authorize(Roles = "Diretor")]
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            return View(new MProfessor().BringAll());
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
        public ActionResult Create(Professor professor)
        {
            professor.ativo = true;
            Usuario novo = new Usuario();
            novo.login = professor.cpfProfessor;
            novo.senha = professor.cpfProfessor;
            novo.idTipoUsuario = (int)Enumeradores.TipoUsuario.Professor;
            professor.Usuario.Add(novo);
            MProfessor mProfessor = new MProfessor();
            mProfessor.Add(professor);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult Update(int id)
        {
            MProfessor mProfessor = new MProfessor();
            return View(mProfessor.BringOne(c => c.idProfessor == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Diretor")]
        public ActionResult Update(Professor professor, bool? tornarDiretor)
        {
            MProfessor mProfessor = new MProfessor();
            Usuario usuarioProfessor = mProfessor.BringOne(c => c.idProfessor == professor.idProfessor).Usuario.FirstOrDefault();
            if (usuarioProfessor.idTipoUsuario != (int)Enumeradores.TipoUsuario.Diretor)
            {
                if (tornarDiretor.HasValue && tornarDiretor.Value)
                {
                    usuarioProfessor.idTipoUsuario = (int)Enumeradores.TipoUsuario.Diretor;
                    MUsuario mUsuario = new MUsuario();
                    mUsuario.Update(usuarioProfessor);
                    int idUsuario = Convert.ToInt32(User.Identity.Name);
                    Usuario usuarioAtual = mUsuario.BringOne(c => c.idUsuario == idUsuario);
                    usuarioAtual.idTipoUsuario = (int)Enumeradores.TipoUsuario.Professor;
                    mUsuario.Update(usuarioAtual);
                }
            }
            TempData["Message"] = mProfessor.Update(professor) ? "Professor atualizado com sucesso" : "Ação não foi realizada";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult Delete(int id)
        {
            MUsuario mUsuario = new MUsuario();
            Professor professor = new MProfessor().BringOne(c => c.idProfessor == id);
            int idUsuario = professor.Usuario.FirstOrDefault().idUsuario;
            Usuario usuario = mUsuario.BringOne(c => c.idUsuario == idUsuario);
            usuario.Professor.Remove(professor);
            professor.Usuario.Remove(usuario);
            mUsuario.Delete(usuario);
            MProfessor mAluno = new MProfessor();
            TempData["Message"] = mAluno.Delete(mAluno.BringOne(c => c.idProfessor == professor.idProfessor)) ? "Professor deletado com sucesso" : "Ação não foi realizada";
            return RedirectToAction("Index");
        }
    }
}