using Pbl.Models;
using Pbl.Models.DbClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pbl.Controllers
{
    public class GerenciarProblemasMinistadosController : Controller
    {
        // GET: GerenciarProblemasMinistados
        public ActionResult Index()
        {
            Usuario user = (Usuario)Session["usuario"];
            if (user == null)
            {
                return RedirectToAction("Login", "Login");
            }
            Professor prof = user.Professor.First();
            List<Grupo> problemasMinistrados = new MGrupo().Bring(c => c.idProfessor == prof.idProfessor);
            return View(problemasMinistrados);
        }

        public ActionResult SelecionarProblema(int idGrupo)
        {
            Grupo grupo = new MGrupo().BringOne(c => c.idGrupo == idGrupo);
            Med med = grupo.Med;
            ViewData["Grupo"] = grupo;
            List<ProblemaXMed> problemas = new MProblemaXMed().Bring(c => c.idMed == med.idMed);
            return View(problemas);
        }

        public ActionResult MontarFichaAvaliativa(int idProblemaXMed, int idGrupo)
        {
            ProblemaXMed problemaXMed = new MProblemaXMed().BringOne(c => c.idProblemaxMed == idProblemaXMed);
            MAvaliacaoTutoria mAvaliacaoTutoria = new MAvaliacaoTutoria();
            List<AvaliacaoTutoria> avaliacaoTutoria = mAvaliacaoTutoria.Bring(c => (c.idGrupo == idGrupo) && (c.idProblemaxMed == idProblemaXMed));
            if (avaliacaoTutoria.Count != 0)
            {
                return RedirectToAction("SelecionarAluno", "GerenciarProblemasMinistados",new { avaliacoes = avaliacaoTutoria });
            }
            List<Modulo> modulos = new MModulo().Bring(c => c.idSemestre == problemaXMed.Med.idSemestre);
            AvaliacaoTutoria novaAvaliacao = new AvaliacaoTutoria();
            novaAvaliacao.idGrupo = idGrupo;
            novaAvaliacao.idProblemaxMed = idProblemaXMed;
            ViewData["idModulo"] = new SelectList(modulos, "idModulo", "descModulo");
            return View(novaAvaliacao);
        }

        public ActionResult CriarAvaliacao(AvaliacaoTutoria novaAvaliacao, int idModulo)
        {
            MAvaliacaoTutoria mAvaliacaoTutoria = new MAvaliacaoTutoria();
            Grupo grupo = new MGrupo().BringOne(c => c.idGrupo == novaAvaliacao.idGrupo);
            List<InscricaoTurma> alunosInscritos = grupo.InscricaoTurma.ToList();
            MControleNotas mControleNotas = new MControleNotas();
            foreach (var inscrito in alunosInscritos)
            {
                ControleNotas controleNotas = mControleNotas.BringOne(c => (c.idInscricaoTurma == inscrito.idInscricaoTurma) && (c.idModulo == idModulo));
                if (controleNotas == null)
                {
                    controleNotas.idModulo = idModulo;
                    controleNotas.idInscricaoTurma = inscrito.idInscricaoTurma;
                    mControleNotas.Add(controleNotas);
                }
                AvaliacaoTutoria avaliacaoAluno = new AvaliacaoTutoria();
                avaliacaoAluno.dtFim = novaAvaliacao.dtFim;
                avaliacaoAluno.dtInicio = novaAvaliacao.dtInicio;
                avaliacaoAluno.idControleNotas = controleNotas.idControleNotas;
                avaliacaoAluno.idGrupo = grupo.idGrupo;
                avaliacaoAluno.idProblemaxMed = novaAvaliacao.idProblemaxMed;
                mAvaliacaoTutoria.Add(avaliacaoAluno);
            }
                List<AvaliacaoTutoria> avaliacoesTutoria = mAvaliacaoTutoria.Bring(c => (c.idGrupo == grupo.idGrupo) && (c.idProblemaxMed == novaAvaliacao.idProblemaxMed));
            return RedirectToAction("SelecionarAluno", "GerenciarProblemasMinistados",new { avaliacoes = avaliacoesTutoria } );
        }

        public ActionResult SelecionarAluno(List<AvaliacaoTutoria> avalicoes)
        {
            if (avalicoes == null)
            {
                avalicoes = new List<AvaliacaoTutoria>();
            }
            return View(avalicoes);
        }
    }
}
