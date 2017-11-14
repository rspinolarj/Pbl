using Newtonsoft.Json;
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
            AvaliacaoTutoria novaAvaliacao = new AvaliacaoTutoria();
            if (avaliacaoTutoria.Count > 0)
            {
                novaAvaliacao = avaliacaoTutoria.FirstOrDefault();
            }
            else
            {
                novaAvaliacao.idGrupo = idGrupo;
                novaAvaliacao.idProblemaxMed = idProblemaXMed;
            }
            List<Modulo> modulos = new MModulo().Bring(c => c.idSemestre == problemaXMed.Med.idSemestre);
            ViewData["idModulo"] = new SelectList(modulos, "idModulo", "descModulo");
            string js = JsonConvert.SerializeObject(modulos, Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });
            ViewData["modulos"] = js;
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
                var teste = (inscrito.ControleNotas.Select(c => c.AvaliacaoTutoria.Where(x => x.idProblemaxMed == novaAvaliacao.idProblemaxMed))).FirstOrDefault().FirstOrDefault();
                AvaliacaoTutoria avaliacaoAluno = teste;
                if (avaliacaoAluno == null)
                {
                    avaliacaoAluno = new AvaliacaoTutoria();
                    avaliacaoAluno.dtFim = novaAvaliacao.dtFim;
                    avaliacaoAluno.dtInicio = novaAvaliacao.dtInicio;
                    avaliacaoAluno.idControleNotas = controleNotas.idControleNotas;
                    avaliacaoAluno.idGrupo = grupo.idGrupo;
                    avaliacaoAluno.idProblemaxMed = novaAvaliacao.idProblemaxMed;
                    mAvaliacaoTutoria.Add(avaliacaoAluno);
                }
                else
                {
                    avaliacaoAluno.dtFim = novaAvaliacao.dtFim;
                    avaliacaoAluno.dtInicio = novaAvaliacao.dtInicio;
                    avaliacaoAluno.idControleNotas = controleNotas.idControleNotas;
                    avaliacaoAluno.idGrupo = grupo.idGrupo;
                    avaliacaoAluno.idProblemaxMed = novaAvaliacao.idProblemaxMed;
                    mAvaliacaoTutoria.Update(avaliacaoAluno);
                }
                
            }
            List<AvaliacaoTutoria> avaliacoesTutoria = mAvaliacaoTutoria.Bring(c => (c.idGrupo == grupo.idGrupo) && (c.idProblemaxMed == novaAvaliacao.idProblemaxMed));
            return RedirectToAction("SelecionarAluno", "GerenciarProblemasMinistrados", new { idProblemaXMed = novaAvaliacao.idProblemaxMed, idGrupo = grupo.idGrupo });
        }

        [Authorize(Roles = "Diretor,Professor")]
        public ActionResult SelecionarAluno(int idProblemaXMed, int idGrupo)
        {
            var mAvaliacaoTutoria = new MAvaliacaoTutoria();
            List<AvaliacaoTutoria> avaliacoes = mAvaliacaoTutoria.Bring(c => (c.idProblemaxMed == idProblemaXMed) && (c.idGrupo == idGrupo));
            ListarAvaliacaoTutoriaViewModel viewModel = new ListarAvaliacaoTutoriaViewModel();
            viewModel.alunosAvaliados = new List<AvaliacaoTutoria>();
            viewModel.alunosNaoAvaliados = new List<AvaliacaoTutoria>();
            Grupo grupo = new MGrupo().BringOne(c => c.idGrupo == idGrupo);
            Problema problema = new MProblemaXMed().BringOne(c => c.idProblemaxMed == idProblemaXMed).Problema;
            ViewBag.Informativo = problema.tituloProblema + " - " + grupo.descGrupo;

            if (avaliacoes.Count == 0)
            {
                return RedirectToAction("MontarFichaAvaliativa", new { idProblemaXMed = idProblemaXMed, idGrupo = idGrupo });
            }
            int idModulo = (int)avaliacoes.FirstOrDefault().ControleNotas.idModulo;
            DateTime dataFimAvaliacao = avaliacoes.FirstOrDefault().dtFim;
            DateTime dataInicioAvaliacao = avaliacoes.FirstOrDefault().dtInicio;
            if (grupo.InscricaoTurma.Count != avaliacoes.Count)
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



            int idUsuario = Convert.ToInt32(HttpContext.User.Identity.Name);
            Usuario user = new MUsuario().BringOne(c => c.idUsuario == idUsuario);
            Professor prof = user.Professor.First();
            if (grupo.idProfessor != prof.idProfessor)
            {
                return View(viewModel);
            }
            if (avaliacoes == null)
            {
                avaliacoes = new List<AvaliacaoTutoria>();
            }
            foreach (AvaliacaoTutoria item in avaliacoes)
            {
                if (item.FichaAvaliacao.Where(c => c.idAvaliador == idUsuario).FirstOrDefault() != null)
                {
                    if (item.FichaAvaliacao.Where(c => c.idAvaliador == idUsuario).FirstOrDefault().PerguntaXFicha.Where(c => c.marcado != null).Count() == 9)
                    {
                        viewModel.alunosAvaliados.Add(item);
                    }
                    else
                    {
                        viewModel.alunosNaoAvaliados.Add(item);
                    }
                }
                else
                {
                    viewModel.alunosNaoAvaliados.Add(item);
                }
            }
            return View(viewModel);
        }

        [Authorize(Roles = "Diretor,Professor")]
        public ActionResult AvaliarAluno(int idAvaliacaoTutoria)
        {
            int idUsuario = Convert.ToInt32(HttpContext.User.Identity.Name);
            AvaliacaoTutoria avaliacaoTutoria = new MAvaliacaoTutoria().BringOne(c => c.idAvaliacaoTutoria == idAvaliacaoTutoria);
            MFichaAvaliacao mFichaAvaliacao = new MFichaAvaliacao();
            FichaAvaliacao fichaAvaliacao = mFichaAvaliacao.BringOne(c => (c.idAvaliacaoTutoria == idAvaliacaoTutoria) && (c.idAvaliador == idUsuario));
            MPerguntaXFicha mPerguntaXFicha = new MPerguntaXFicha();
            if (fichaAvaliacao == null)
            {
                Usuario user = new MUsuario().BringOne(c => c.idUsuario == idUsuario);
                Professor prof = user.Professor.First();
                if (avaliacaoTutoria.Grupo.idProfessor == prof.idProfessor)
                {
                    FichaAvaliacao nova = new FichaAvaliacao();
                    nova.idAvaliacaoTutoria = idAvaliacaoTutoria;
                    nova.idAvaliador = user.idUsuario;
                    mFichaAvaliacao.Add(nova);
                    List<Pergunta> perguntasFicha = new MPergunta().BringAll();

                    foreach (Pergunta item in perguntasFicha)
                    {
                        PerguntaXFicha perguntaXFicha = new PerguntaXFicha();
                        perguntaXFicha.idFichaAvaliacao = nova.idFichaAvaliacao;
                        perguntaXFicha.idPergunta = item.idPergunta;
                        perguntaXFicha.marcado = null;
                        mPerguntaXFicha.Add(perguntaXFicha);
                    }
                }
            }
            List<PerguntaXFicha> perguntas = mPerguntaXFicha.Bring(c => c.idFichaAvaliacao == fichaAvaliacao.idFichaAvaliacao);
            return View(perguntas);
        }


        [Authorize(Roles = "Diretor,Professor")]
        public ActionResult InserirNotas(int idFichaAvaliacao, int[] perguntas, bool[] respostas)
        {
            MPerguntaXFicha mPerguntaXFicha = new MPerguntaXFicha();
            for (int i = 0; i < perguntas.Length; i++)
            {
                int idPergunta = perguntas[i];
                PerguntaXFicha perguntaXFicha = mPerguntaXFicha.BringOne(c => (c.idFichaAvaliacao == idFichaAvaliacao) && (c.idPergunta == idPergunta));
                perguntaXFicha.marcado = respostas[i];
                mPerguntaXFicha.Update(perguntaXFicha);
            }
            FichaAvaliacao fichaAvaliacao = new MFichaAvaliacao().BringOne(c => c.idFichaAvaliacao == idFichaAvaliacao);
            return RedirectToAction("SelecionarAluno", new { idProblemaXMed = fichaAvaliacao.AvaliacaoTutoria.idProblemaxMed, idGrupo = fichaAvaliacao.AvaliacaoTutoria.idGrupo });
        }
    }
}
