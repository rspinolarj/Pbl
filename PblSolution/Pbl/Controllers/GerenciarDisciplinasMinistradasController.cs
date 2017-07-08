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
    public class GerenciarDisciplinasMinistradasController : Controller
    {
        // GET: GerenciarDisciplinasMinistradas
        public ActionResult Index()
        {
            Usuario user = (Usuario)Session["usuario"];
            if (user == null)
            {
                return RedirectToAction("Login", "Login");
            }
            Professor prof = user.Professor.First();
            List<Aula> aulasMinistradas = new MAula().Bring(c => c.idProfessor == prof.idProfessor);
            return View(aulasMinistradas);
        }

        public ActionResult SelecionarModulo(int idAula)
        {
            Med med = new MAula().BringOne(c => c.idAula == idAula).Turma.Med;
            Aula aula = new MAula().BringOne(c => c.idAula == idAula);
            ViewData["Aula"] = aula;
            List<Modulo> listModulos = med.Semestre.Modulo.ToList();
            return View(listModulos);
        }

        public ActionResult SelecionarAlunos(int idAula, int idModulo)
        {
            Med med = new MAula().BringOne(c => c.idAula == idAula).Turma.Med;
            Modulo modulo = new MModulo().BringOne(c => c.idModulo == idModulo);
            Aula aula = new MAula().BringOne(c => c.idAula == idAula);
            Turma turma = aula.Turma;
            List<InscricaoTurma> alunosInscritos = new MInscricaoTurma().Bring(c => c.idTurma == turma.idTurma);
            List<SelecionarAlunosViewModel> viewModel = new List<SelecionarAlunosViewModel>();
            MControleNotas mControleNotas = new MControleNotas();
            MControleNotasXAula mControleNotasXAula = new MControleNotasXAula();
            foreach (var inscrito in alunosInscritos)
            {
                ControleNotas controleNotas = mControleNotas.BringOne(c => (c.idInscricaoTurma == inscrito.idInscricaoTurma) && (c.idModulo == idModulo));
                if (controleNotas == null)
                {
                    controleNotas = new ControleNotas() { idModulo = idModulo, idInscricaoTurma = inscrito.idInscricaoTurma };
                    mControleNotas.Add(controleNotas);
                }
                ControleNotasXAula controleNotasXAula = mControleNotasXAula.BringOne(c => (c.idAula == idAula) && (c.idControleNotas == controleNotas.idControleNotas));
                SelecionarAlunosViewModel novo = new SelecionarAlunosViewModel();
                novo.inscricao = inscrito;
                if (controleNotasXAula != null)
                {
                    novo.nota = controleNotasXAula.nota;
                }
                viewModel.Add(novo);
            }
            ViewData["Aula"] = aula;
            ViewData["Modulo"] = modulo;
            return View(viewModel);
        }

        public ActionResult AvaliarAluno(int idInscricaoTurma, int idModulo, int idAula)
        {
            Med med = new MAula().BringOne(c => c.idAula == idAula).Turma.Med;
            Modulo modulo = new MModulo().BringOne(c => c.idModulo == idModulo);
            Aula aula = new MAula().BringOne(c => c.idAula == idAula);
            ViewData["Aula"] = aula;
            ViewData["Modulo"] = modulo;
            ViewData["Aluno"] = new MInscricaoTurma().BringOne(c => c.idInscricaoTurma == idInscricaoTurma).Aluno;
            ControleNotas controleNotas = new MControleNotas().BringOne(c => (c.idInscricaoTurma == idInscricaoTurma) && (c.idModulo == idModulo));
            ControleNotasXAula controleNotasAula = new ControleNotasXAula();
            controleNotasAula.idAula = idAula;
            //controleNotasAula.nota = nota;
            controleNotasAula.idControleNotas = controleNotas.idControleNotas;
            MControleNotasXAula mControleNotasXAula = new MControleNotasXAula();
            return View(controleNotasAula);
        }

        public ActionResult AvaliarAlunoAction(ControleNotasXAula controleNotasAula)
        {
            MControleNotasXAula mControleNotasXAula = new MControleNotasXAula();
            if (!(mControleNotasXAula.Add(controleNotasAula)))
            {
                mControleNotasXAula.Update(controleNotasAula);
            }

            return RedirectToAction("SelecionarAlunos", "GerenciarDisciplinasMinistradas",new {IdAula = controleNotasAula.idAula, IdModulo = controleNotasAula.ControleNotas.idModulo });
        }
    }
}
