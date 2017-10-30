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
            /*int idUsuario = Convert.ToInt32(HttpContext.User.Identity.Name);
            Usuario user = new MUsuario().BringOne(c => c.idUsuario == idUsuario);
            Aluno aluno = user.Aluno.FirstOrDefault();
            List<InscricaoTurma> turmasAluno = aluno.InscricaoTurma.ToList();*/
            List<ListagemMedsAlunoViewModel> viewModel = new List<ListagemMedsAlunoViewModel>();
            viewModel.Add(new ListagemMedsAlunoViewModel()
            {
                descMed = "MED 1",
                descSemestre = "2016.2",
                idMed = 1,
                notas = new int[] { 88, 65, 90 },     
                idControleNotas = new int[] {1,1,1,1,}
            });
            viewModel.Add(new ListagemMedsAlunoViewModel()
            {
                descMed = "MED 2",
                descSemestre = "2017.1",
                idMed = 2,
                notas = new int[] { 70, 82, 60 },
                idControleNotas = new int[] { 1, 1, 1, 1, }
            });
            viewModel.Add(new ListagemMedsAlunoViewModel()
            {
                descMed = "MED 3",
                descSemestre = "2017.2",
                idMed = 3,
                notas = new int[] { 100, 85, 80 },
                idControleNotas = new int[] { 1, 1, 1, 1, }
            });
            return View(viewModel);
        }

        [Authorize(Roles = "Aluno")]
        public ActionResult DetalhesMed(int idMed)
        {
            /*InscricaoTurma inscricaoTurma = new MInscricaoTurma().BringOne(c => c.idInscricaoTurma == id);
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
            }*/
            ViewBag.DescMed = "MED 2 - 2017.2";
            List<DetalhesModuloAlunoViewModel> viewModel = new List<DetalhesModuloAlunoViewModel>();
            viewModel.Add(new DetalhesModuloAlunoViewModel()
            {
                descModulo = "Módulo 1",
                notaSimuladoMorfofuncional = 50,
                notaSimuladoTutoria = 70,
                disciplinas = new List<DetalhesDisciplinaAlunoViewModel>
                {
                    new DetalhesDisciplinaAlunoViewModel()
                    {
                        descDisciplina = "Anatomia",
                        nota = 70,
                        tipoAvaliacao = "Pratica"
                    },
                    new DetalhesDisciplinaAlunoViewModel
                    {
                        descDisciplina = "Histologia",
                        nota = 100,
                        tipoAvaliacao = "Pratica"
                    },
                    new DetalhesDisciplinaAlunoViewModel()
                    {
                        descDisciplina = "Farmaco",
                        nota = 85,
                        tipoAvaliacao = "Formativa"

                    },
                },
                problemas = new List<DetalhesProblemaAlunoViewModel>
                {
                    new DetalhesProblemaAlunoViewModel()
                    {
                        tituloProblema = "Problema 1",
                        notaFinal = 25
                    },
                    new DetalhesProblemaAlunoViewModel()
                    {
                        tituloProblema = "Problema 2",
                        notaFinal = 30
                    }
                }
            });
            viewModel.Add(new DetalhesModuloAlunoViewModel()
            {
                descModulo = "Módulo 2",
                notaSimuladoMorfofuncional = 50,
                notaSimuladoTutoria = 70,
                disciplinas = new List<DetalhesDisciplinaAlunoViewModel>
                {
                    new DetalhesDisciplinaAlunoViewModel()
                    {
                        descDisciplina = "Anatomia",
                        nota = 75,
                        tipoAvaliacao = "Pratica"
                    },
                    new DetalhesDisciplinaAlunoViewModel
                    {
                        descDisciplina = "Histologia",
                        nota = 89,
                        tipoAvaliacao = "Pratica"
                    },
                    new DetalhesDisciplinaAlunoViewModel()
                    {
                        descDisciplina = "Farmaco",
                        nota = 95,
                        tipoAvaliacao = "Formativa"
                    },
                },
                problemas = new List<DetalhesProblemaAlunoViewModel>
                {
                    new DetalhesProblemaAlunoViewModel()
                    {
                        tituloProblema = "Problema 3",
                        notaFinal = 25
                    },
                    new DetalhesProblemaAlunoViewModel()
                    {
                        tituloProblema = "Problema 4",
                        notaFinal = 30
                    }
                }
            });
            viewModel.Add(new DetalhesModuloAlunoViewModel()
            {
                descModulo = "Módulo 3",
                notaSimuladoMorfofuncional = 50,
                notaSimuladoTutoria = 70,
                disciplinas = new List<DetalhesDisciplinaAlunoViewModel>
                {
                    new DetalhesDisciplinaAlunoViewModel()
                    {
                        descDisciplina = "Anatomia",
                        nota = 70,
                        tipoAvaliacao = "Pratica"
                    },
                    new DetalhesDisciplinaAlunoViewModel
                    {
                        descDisciplina = "Histologia",
                        nota = 80,
                        tipoAvaliacao = "Pratica"
                    },
                    new DetalhesDisciplinaAlunoViewModel()
                    {
                        descDisciplina = "Farmaco",
                        nota = 80,
                        tipoAvaliacao = "Formativa"

                    },
                },
                problemas = new List<DetalhesProblemaAlunoViewModel>
                {
                    new DetalhesProblemaAlunoViewModel()
                    {
                        tituloProblema = "Problema 5",
                        notaFinal = 25
                    },
                    new DetalhesProblemaAlunoViewModel()
                    {
                        tituloProblema = "Problema 6",
                        notaFinal = 30
                    }
                }
            });
            return View(viewModel);
        }

        [Authorize(Roles = "Aluno")]
        public ActionResult DetalhesModulo(int idControleNotas)
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

            /*

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
            return View(detalhesModuloViewModel);*/
            var viewModel = new DetalhesModuloAlunoViewModel()
            {
                descModulo = "MED 2 - 2017.2 - Módulo 2",
                notaSimuladoMorfofuncional = 50,
                notaSimuladoTutoria = 70,
                disciplinas = new List<DetalhesDisciplinaAlunoViewModel>
                {
                    new DetalhesDisciplinaAlunoViewModel()
                    {
                        descDisciplina = "Anatomia",
                        nota = 75,
                        tipoAvaliacao = "Pratica"
                    },
                    new DetalhesDisciplinaAlunoViewModel
                    {
                        descDisciplina = "Histologia",
                        nota = 89,
                        tipoAvaliacao = "Pratica"
                    },
                    new DetalhesDisciplinaAlunoViewModel()
                    {
                        descDisciplina = "Farmaco",
                        nota = 95,
                        tipoAvaliacao = "Formativa"
                    },
                },
                problemas = new List<DetalhesProblemaAlunoViewModel>
                {
                    new DetalhesProblemaAlunoViewModel()
                    {
                        tituloProblema = "Problema 3",
                        notaFinal = 25
                    },
                    new DetalhesProblemaAlunoViewModel()
                    {
                        tituloProblema = "Problema 4",
                        notaFinal = 30
                    }
                }
            };
            return View(viewModel);
        }

        [Authorize(Roles = "Aluno")]
        public ActionResult AvaliacoesEmAberto()
        {
            /*int idUsuario = Convert.ToInt32(HttpContext.User.Identity.Name);
            Usuario user = new MUsuario().BringOne(c => c.idUsuario == idUsuario);
            Aluno aluno = user.Aluno.FirstOrDefault();
            DateTime hoje = DateTime.Today;
            InscricaoTurma inscricaoTurma = aluno.InscricaoTurma.FirstOrDefault(c => (c.Turma.Med.Semestre.Modulo.First().dtInicio < hoje) && (c.Turma.Med.Semestre.Modulo.Last().dtFim > hoje));
            List<AvaliacaoTutoria> avaliacoes = inscricaoTurma.ControleNotas.SelectMany(c => c.AvaliacaoTutoria.Where(x => (x.dtInicio < hoje) && (x.dtFim > hoje))).ToList();
            return View(avaliacoes);*/
            return null;
        }
    }
}