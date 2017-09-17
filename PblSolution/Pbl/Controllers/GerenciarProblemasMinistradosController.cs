using Pbl.Models;
using Pbl.Models.DbClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Pbl.Controllers
{
    public class GerenciarProblemasMinistradosController : Controller
    {
        // GET: GerenciarProblemasMinistados
        [Authorize(Roles = "Diretor,Professor")]
        public ActionResult Index()
        {
            int idUsuario = Convert.ToInt32(HttpContext.User.Identity.Name);
            Usuario user = new MUsuario().BringOne(c => c.idUsuario == idUsuario);
            Professor prof = user.Professor.First();
            List<Grupo> problemasMinistrados = new MGrupo().Bring(c => c.idProfessor == prof.idProfessor);
            return View(problemasMinistrados);
        }

        [Authorize(Roles = "Diretor,Professor")]
        public ActionResult SelecionarProblema(int idGrupo)
        {
            Grupo grupo = new MGrupo().BringOne(c => c.idGrupo == idGrupo);
            Med med = grupo.Med;
            ViewData["Grupo"] = grupo;
            List<ProblemaXMed> problemas = new MProblemaXMed().Bring(c => c.idMed == med.idMed);
            return View(problemas);
        }

        [Authorize(Roles = "Diretor,Professor")]
        public ActionResult MontarFichaAvaliativa(int idProblemaXMed, int idGrupo)
        {
            ProblemaXMed problemaXMed = new MProblemaXMed().BringOne(c => c.idProblemaxMed == idProblemaXMed);
            MAvaliacaoTutoria mAvaliacaoTutoria = new MAvaliacaoTutoria();
            List<AvaliacaoTutoria> avaliacaoTutoria = mAvaliacaoTutoria.Bring(c => (c.idGrupo == idGrupo) && (c.idProblemaxMed == idProblemaXMed));
            Grupo grupo = new MGrupo().BringOne(c => c.idGrupo == idGrupo);
            if (avaliacaoTutoria.Count != 0)
            {
                int idModulo = (int)avaliacaoTutoria.FirstOrDefault().ControleNotas.idModulo;
                DateTime dataFimAvaliacao = avaliacaoTutoria.FirstOrDefault().dtFim.Value;
                DateTime dataInicioAvaliacao = avaliacaoTutoria.FirstOrDefault().dtInicio.Value;
                if (grupo.InscricaoTurma.Count != avaliacaoTutoria.Count)
                {
                    List<InscricaoTurma> alunosInscritos = grupo.InscricaoTurma.ToList();
                    MControleNotas mControleNotas = new MControleNotas();
                    foreach (InscricaoTurma alunoInscrito in alunosInscritos)
                    {
                        ControleNotas controleNotas = alunoInscrito.ControleNotas.SingleOrDefault(c => c.idModulo == idModulo);
                        if (controleNotas == null)
                        {
                            controleNotas.idModulo = idModulo;
                            controleNotas.idInscricaoTurma = alunoInscrito.idInscricaoTurma;
                            mControleNotas.Add(controleNotas);
                            AvaliacaoTutoria avaliacaoAluno = new AvaliacaoTutoria();
                            avaliacaoAluno.dtFim = dataFimAvaliacao;
                            avaliacaoAluno.dtInicio = dataInicioAvaliacao;
                            avaliacaoAluno.idControleNotas = controleNotas.idControleNotas;
                            avaliacaoAluno.idGrupo = grupo.idGrupo;
                            avaliacaoAluno.idProblemaxMed = idProblemaXMed;
                            mAvaliacaoTutoria.Add(avaliacaoAluno);
                        }
                        else
                        {
                            if (alunoInscrito.ControleNotas.SingleOrDefault(c => c.idModulo == idModulo).AvaliacaoTutoria.SingleOrDefault(c => c.idProblemaxMed == idProblemaXMed) == null)
                            {
                                AvaliacaoTutoria avaliacaoAluno = new AvaliacaoTutoria();
                                avaliacaoAluno.dtFim = dataFimAvaliacao;
                                avaliacaoAluno.dtInicio = dataInicioAvaliacao;
                                avaliacaoAluno.idControleNotas = controleNotas.idControleNotas;
                                avaliacaoAluno.idGrupo = grupo.idGrupo;
                                avaliacaoAluno.idProblemaxMed = idProblemaXMed;
                                mAvaliacaoTutoria.Add(avaliacaoAluno);
                            }
                        }

                    }
                }
                return RedirectToAction("SelecionarAluno", "GerenciarProblemasMinistrados", new { idProblemaXMed = idProblemaXMed, idGrupo = idGrupo });
            }
            List<Modulo> modulos = new MModulo().Bring(c => c.idSemestre == problemaXMed.Med.idSemestre);
            AvaliacaoTutoria novaAvaliacao = new AvaliacaoTutoria();
            novaAvaliacao.idGrupo = idGrupo;
            novaAvaliacao.idProblemaxMed = idProblemaXMed;
            ViewData["idModulo"] = new SelectList(modulos, "idModulo", "descModulo");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ViewData["modulos"] = serializer.Serialize(modulos);
            return View(novaAvaliacao);
        }

        [Authorize(Roles = "Diretor,Professor")]
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
            return RedirectToAction("SelecionarAluno", "GerenciarProblemasMinistrados", new { avaliacoes = avaliacoesTutoria });
        }

        [Authorize(Roles = "Diretor,Professor")]
        public ActionResult SelecionarAluno(int idProblemaXMed, int idGrupo)
        {
            List<AvaliacaoTutoria> avaliacoes = new MAvaliacaoTutoria().Bring(c => (c.idProblemaxMed == idProblemaXMed) && (c.idGrupo == idGrupo));
            if (avaliacoes == null)
            {
                avaliacoes = new List<AvaliacaoTutoria>();
            }
            return View(avaliacoes);
        }

        [Authorize(Roles = "Diretor,Professor")]
        public ActionResult AvaliarAluno(int idAvaliacaoTutoria)
        {
            AvaliacaoTutoria avaliacaoTutoria = new MAvaliacaoTutoria().BringOne(c => c.idAvaliacaoTutoria == idAvaliacaoTutoria);
            return View();
        }
    }
}
