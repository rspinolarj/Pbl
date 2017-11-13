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
    public class AcompanhamentoMedsController : Controller
    {
        // GET: AcompanhamentoMeds
        [Authorize(Roles = "Aluno")]
        public ActionResult Index()
        {
            int idUsuario = Convert.ToInt32(HttpContext.User.Identity.Name);
            Usuario user = new MUsuario().BringOne(c => c.idUsuario == idUsuario);
            Aluno aluno = user.Aluno.FirstOrDefault();
            List<InscricaoTurma> turmasAluno = aluno.InscricaoTurma.ToList();
            MControleNotas mControleNotas = new MControleNotas();
            List<ListagemMedsAlunoViewModel> viewModel = new List<ListagemMedsAlunoViewModel>();
            foreach (var med in turmasAluno)
            {
                var itemAdciononar = new ListagemMedsAlunoViewModel
                {
                    descMed = med.Turma.Med.descMed,
                    descSemestre = med.ControleNotas.FirstOrDefault().Modulo.Semestre.descSemestre,
                    idMed = med.Turma.idMed,
                    notas = new double[] { 0, 0, 0 },
                    idControleNotas = new int[] { 0, 0, 0 }
                };
                for (int i = 0; i < med.ControleNotas.Count; i++)
                {
                    itemAdciononar.notas[i] = mControleNotas.RetornaNota(med.ControleNotas.ElementAt(i).idControleNotas);
                    itemAdciononar.idControleNotas[i] = med.ControleNotas.ElementAt(i).idControleNotas;
                }
                viewModel.Add(itemAdciononar);
            }
            return View(viewModel);
        }

        [Authorize(Roles = "Aluno")]
        public ActionResult DetalhesModulo(int idControleNotas)
        {
            var viewModel = new DetalhesModuloAlunoViewModel();
            MControleNotas mControleNotas = new MControleNotas();
            ControleNotas controleNotas = mControleNotas.BringOne(c => c.idControleNotas == idControleNotas);
            viewModel.descModulo = controleNotas.InscricaoTurma.Turma.Med.descMed + " - " + controleNotas.InscricaoTurma.Turma.Med.Semestre.descSemestre + " - " + controleNotas.Modulo.descModulo;
            viewModel.notaSimuladoMorfofuncional = mControleNotas.retornaNotaSimulado(controleNotas.idControleNotas, controleNotas.ControleNotasXProva.Where(c => c.Prova.idTipoProva == (int)Enumeradores.TipoProva.Morfofuncional).Select(c => c.idProva).FirstOrDefault());
            viewModel.notaSimuladoTutoria = mControleNotas.retornaNotaSimulado(controleNotas.idControleNotas, controleNotas.ControleNotasXProva.Where(c => c.Prova.idTipoProva == (int)Enumeradores.TipoProva.Tutoria).Select(c => c.idProva).FirstOrDefault());
            viewModel.disciplinas = new List<DetalhesDisciplinaAlunoViewModel>();
            foreach (var disciplina in controleNotas.ControleNotasXAula)
            {
                viewModel.disciplinas.Add(new DetalhesDisciplinaAlunoViewModel()
                {
                    descDisciplina = disciplina.Aula.Disciplina.descDisciplina,
                    nota = mControleNotas.retornaNotaDisciplina(disciplina.idAula, controleNotas.idControleNotas),
                    tipoAvaliacao = disciplina.Aula.Disciplina.TipoDisciplina.descTipoDisciplina
                });
            }
            viewModel.problemas = new List<DetalhesProblemaAlunoViewModel>();
            foreach (var problema in controleNotas.AvaliacaoTutoria)
            {
                viewModel.problemas.Add(new DetalhesProblemaAlunoViewModel()
                {
                    tituloProblema = problema.ProblemaXMed.Problema.descProblema,
                    notaFinal = mControleNotas.retornaNotaProblema(problema.idAvaliacaoTutoria)
                });
            }
            return View(viewModel);
        }

        [Authorize(Roles = "Aluno")]
        public ActionResult AvaliacoesEmAberto()
        {
            int idUsuario = Convert.ToInt32(HttpContext.User.Identity.Name);
            Usuario user = new MUsuario().BringOne(c => c.idUsuario == idUsuario);
            Aluno aluno = user.Aluno.FirstOrDefault();
            DateTime hoje = DateTime.Today;
            InscricaoTurma inscricaoTurma = aluno.InscricaoTurma.FirstOrDefault(c => (c.Turma.Med.Semestre.Modulo.First().dtInicio < hoje) && (c.Turma.Med.Semestre.Modulo.Last().dtFim > hoje));
            List<AvaliacaoTutoria> avaliacoes = inscricaoTurma.ControleNotas.SelectMany(c => c.AvaliacaoTutoria/*.Where(x => (x.dtInicio < hoje) && (x.dtFim > hoje))*/).ToList();
            return View(avaliacoes);
        }

        [Authorize(Roles = "Aluno")]
        public ActionResult AvaliacoesInterpares(int idGrupo, int idProblemaXMed)
        {
            List<AvaliacaoTutoria> avaliacoes = new MAvaliacaoTutoria().Bring(c => (c.idProblemaxMed == idProblemaXMed) && (c.idGrupo == idGrupo));
            ListarAvaliacaoTutoriaViewModel viewModel = new ListarAvaliacaoTutoriaViewModel();
            viewModel.alunosAvaliados = new List<AvaliacaoTutoria>();
            viewModel.alunosNaoAvaliados = new List<AvaliacaoTutoria>();
            Grupo grupo = new MGrupo().BringOne(c => c.idGrupo == idGrupo);
            int idUsuario = Convert.ToInt32(HttpContext.User.Identity.Name);
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

        [Authorize(Roles = "Aluno")]
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
            List<PerguntaXFicha> perguntas = mPerguntaXFicha.Bring(c => c.idFichaAvaliacao == fichaAvaliacao.idFichaAvaliacao);
            return View(perguntas);
        }

        [Authorize(Roles = "Aluno")]
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