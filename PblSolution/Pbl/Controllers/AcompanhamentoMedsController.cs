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
            return View(turmasAluno);
        }

        [Authorize(Roles = "Aluno")]
        public ActionResult DetalhesMed(int id)
        {
            InscricaoTurma inscricaoTurma = new MInscricaoTurma().BringOne(c => c.idInscricaoTurma == id);
            MControleNotas mControleNotas = new MControleNotas();
            GerenciarNotasViewModel viewModel = new GerenciarNotasViewModel();
            viewModel.controleNotas = new List<ControleNotasViewModel>();
            foreach (var item in inscricaoTurma.ControleNotas)
            {
                ControleNotasViewModel novo = new ControleNotasViewModel()
                {
                    controleNotas = item,
                    nota = mControleNotas.RetornaNota(item.idControleNotas)
                };
                viewModel.controleNotas.Add(novo);
            }
            return View(viewModel);
        }

        [Authorize(Roles = "Aluno")]
        public ActionResult DetalhesModulo(int id)
        {
            /*
             List<Turma> listTurmas = new MTurma().Bring(c => c.idMed == idMed);
            List<InscricaoTurma> listAlunosInscritos = new List<InscricaoTurma>();
            MInscricaoTurma mInscricaoTurma = new MInscricaoTurma();
            foreach (var turma in listTurmas)
            {
                listAlunosInscritos.AddRange(mInscricaoTurma.Bring(c => c.idTurma == turma.idTurma));
            }
            List<GerenciarNotasViewModel> listGerenciaNotas = new List<GerenciarNotasViewModel>();
            MControleNotas mControleNotas = new MControleNotas();
            foreach (var alunoInscrito in listAlunosInscritos)
            {
                GerenciarNotasViewModel notasAluno = new GerenciarNotasViewModel();
                notasAluno.controleNotas = new List<ControleNotasViewModel>();
                foreach (var controleNotas in alunoInscrito.ControleNotas)
                {
                    notasAluno.controleNotas.Add(new ControleNotasViewModel()
                    {
                        controleNotas = controleNotas,
                        nota = mControleNotas.RetornaNota(controleNotas.idControleNotas)
                    });
                }
                notasAluno.aluno = alunoInscrito;
                listGerenciaNotas.Add(notasAluno);
            }
             */



            ControleNotas controleNotas = new MControleNotas().BringOne(c => c.idControleNotas == id);
            DetalhesModuloViewModel detalhesModuloViewModel = new DetalhesModuloViewModel();
            detalhesModuloViewModel.avaliacoesTutoria = new List<NotasProblemaViewModel>();
            List<AvaliacaoTutoria> avaliacoesTutoria = new MAvaliacaoTutoria().Bring(c => c.idControleNotas == controleNotas.idControleNotas);
            foreach (var item in avaliacoesTutoria)
            {
                NotasProblemaViewModel notasProblemaViewModel = new NotasProblemaViewModel();
                notasProblemaViewModel.problema = item.ProblemaXMed.Problema.descProblema;
                notasProblemaViewModel.emAberto = DateTime.Today < item.dtFim && DateTime.Today > item.dtInicio;
                //decimal notaAutoAvaliacao = item.FichaAvaliacao.SingleOrDefault(c => c.idInscricaoTurma == item.ControleNotas.idInscricaoTurma).nota.HasValue ? item.FichaAvaliacao.SingleOrDefault(c => c.idInscricaoTurma == item.ControleNotas.idInscricaoTurma).nota.Value : 0;
                //decimal notaInterpares = item.FichaAvaliacao.Where(c => c.idInscricaoTurma != item.ControleNotas.idInscricaoTurma).Sum(c => c.nota).HasValue ? item.FichaAvaliacao.Where(c => c.idInscricaoTurma != item.ControleNotas.idInscricaoTurma).Sum(c => c.nota).Value / (item.FichaAvaliacao.Count - 1) : 0;
                //notasProblemaViewModel.nota = (notaProfessor + notaAutoAvaliacao + notaInterpares) / 3;
            }
            detalhesModuloViewModel.avaliacoesAula = new MControleNotasXAula().Bring(c => c.idControleNotas == controleNotas.idControleNotas);
            detalhesModuloViewModel.avaliacoesProva = new MControleNotasXProva().Bring(c => c.idControleNotas == controleNotas.idControleNotas);
            return View(detalhesModuloViewModel);
        }

        [Authorize(Roles = "Aluno")]
        public ActionResult AvaliacoesEmAberto()
        {
            int idUsuario = Convert.ToInt32(HttpContext.User.Identity.Name);
            Usuario user = new MUsuario().BringOne(c => c.idUsuario == idUsuario);
            Aluno aluno = user.Aluno.FirstOrDefault();
            DateTime hoje = DateTime.Today;
            InscricaoTurma inscricaoTurma = aluno.InscricaoTurma.FirstOrDefault(c => (c.Turma.Med.Semestre.Modulo.First().dtInicio < hoje) && (c.Turma.Med.Semestre.Modulo.Last().dtFim > hoje));
            List<AvaliacaoTutoria> avaliacoes = inscricaoTurma.ControleNotas.SelectMany(c => c.AvaliacaoTutoria.Where(x => (x.dtInicio < hoje) && (x.dtFim > hoje))).ToList();
            return View(avaliacoes);
        }

    }
}