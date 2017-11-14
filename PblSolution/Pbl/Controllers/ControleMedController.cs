using Pbl.Models;
using Pbl.Models.DbClasses;
using Pbl.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Pbl.Controllers
{
    public class ControleMedController : Controller
    {
        // GET: ControleMed
        [Authorize(Roles = "Diretor")]
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            return View(new MMed().BringAll());
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult GerenciarMed(int id)
        {
            Med med = new MMed().BringOne(c => c.idMed == id);

            if (med.Disciplina.Count == 0)
            {
                return RedirectToAction("AdicionarDisciplinas", "ControleMed", new { idMed = med.idMed });
            }
            ViewBag.Message = TempData["Message"];
            return View(med);
        }

        #region Disciplinas
        public ActionResult AdicionarDisciplinas(int idMed)
        {
            AdicionarDisciplinasViewModel viewModel = new AdicionarDisciplinasViewModel();
            viewModel.med = new MMed().BringOne(c => c.idMed == idMed);
            viewModel.disciplinasDisponiveis = new MDisciplina().RetornaDisciplinasDisponiveis(idMed);
            viewModel.disciplinasCadastradas = viewModel.med.Disciplina.ToList();
            return View(viewModel);
        }

        public ActionResult VincularDisciplinasMed()
        {
            var keys = Request.Form.AllKeys;
            int idMed = Convert.ToInt32(Request.Form["idMed"]);
            var disciplinas = Request.Form["disciplina[]"].Split(',');
            MDisciplina mdisciplina = new MDisciplina();
            List<Disciplina> listDisciplinasVincular = new List<Disciplina>();
            foreach (var disciplina in disciplinas)
            {
                int idDisciplina = Convert.ToInt32(disciplina);
                listDisciplinasVincular.Add(mdisciplina.BringOne(c => c.idDisciplina == idDisciplina));
            }
            MMed mMed = new MMed();
            Med med = mMed.BringOne(c => c.idMed == idMed);
            int[] idsDisciplinasRemover = med.Disciplina.Where(c => !listDisciplinasVincular.Exists(x => x.idDisciplina == c.idDisciplina)).Select(c => c.idDisciplina).ToArray();
            List<Disciplina> listDisciplinasRemover = new List<Disciplina>();
            foreach (var disciplina in idsDisciplinasRemover)
            {
                int idDisciplina = Convert.ToInt32(disciplina);
                Disciplina disciplinaRemover = mdisciplina.BringOne(c => c.idDisciplina == idDisciplina);
                if (disciplinaRemover.Aula.Where(c => c.Turma.idMed == idMed).Count() > 0)
                {
                    continue;
                }
                listDisciplinasRemover.Add(disciplinaRemover);
            }
            listDisciplinasVincular = listDisciplinasVincular.Where(c => !med.Disciplina.ToList().Exists(x => x.idDisciplina == c.idDisciplina)).ToList();
            List<Disciplina> listDisciplinasVinculadas = med.Disciplina.ToList();
            mMed.DesvincularDisciplinas(med.idMed, listDisciplinasRemover.Select(c => c.idDisciplina).ToArray());
            mMed.AdicionarDisciplinas(med.idMed, listDisciplinasVincular.Select(c => c.idDisciplina).ToArray());
            return RedirectToAction("GerenciarMed", new { id = idMed });
        }
        #endregion

        #region Turmas
        [Authorize(Roles = "Diretor")]
        public ActionResult GerenciarTurmas(int idMed)
        {
            MTurma mTurma = new MTurma();
            var turmas = mTurma.Bring(c => c.idMed == idMed);
            Med med = new MMed().BringOne(c => c.idMed == idMed);
            ViewBag.idMed = idMed;
            ViewBag.descMedSemestre = med.descMed + " - " + med.Semestre.descSemestre;
            return View(new MTurma().Bring(c => c.idMed == idMed));
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult AdicionarTurma(int idMed)
        {

            ViewData["idProfessor"] = new SelectList(new MProfessor().BringAll(), "idProfessor", "nomeProfessor");
            ViewData["disciplinasMinistradas"] = new SelectList(new MMed().BringOne(c => c.idMed == idMed).Disciplina, "idDisciplina", "descDisciplina");
            ViewBag.Message = TempData["Message"];
            Turma nova = new Turma();
            nova.idMed = idMed;
            return View(nova);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Diretor")]
        public ActionResult AdicionarNovaTurma(Turma nova, int[] idDisciplina, int[] idProfessor)
        {
            nova.ativo = true;
            TempData["Message"] = new MTurma().Add(nova) ? "Nova Turma Cadastrada" : "Algo Errado Ocorreu";
            MAula mAula = new MAula();
            for (int i = 0; i < idDisciplina.Length; i++)
            {
                Aula aula = new Aula();
                aula.idDisciplina = idDisciplina[i];
                aula.idProfessor = idProfessor[i];
                aula.idTurma = nova.idTurma;
                mAula.Add(aula);
            }

            //GerenciarMedViewModel dados = new GerenciarMedViewModel();
            /*dados.problemasCadastrados = new MProblemaXMed().RetornaProblemasCadastrados((int)nova.idMed);
            dados.turmasCadastradas = new MTurma().Bring(c => c.idMed == nova.idMed);
            dados.med = new MMed().BringOne(c => c.idMed == nova.idMed);*/
            return RedirectToAction("GerenciarTurmas", new { idMed = nova.idMed });
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult ControleTurma()
        {
            InscricaoAlunoViewModel viewModel = new InscricaoAlunoViewModel();
            viewModel.alunosCadastrados = new MAluno().BringAll();
            viewModel.alunosDisponiveis = new List<Aluno>();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Diretor")]
        public ActionResult ControleTurma(ICollection<int> alunos)
        {
            MAluno mAluno = new MAluno();
            foreach (var aluno in alunos)
            {
                //Console.WriteLine(aluno.nomeAluno);
                Console.WriteLine(mAluno.BringOne(c => c.idAluno == aluno).nomeAluno);
            }
            return View();
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult AdicionarAlunosTurma(int idTurma)
        {
            AlunosTurmaViewModel viewModel = new AlunosTurmaViewModel();
            MTurma mTurma = new MTurma();
            MInscricaoTurma mInscricaoTurma = new MInscricaoTurma();
            MAluno mAluno = new MAluno();
            List<Aluno> AlunosCadastrados = mInscricaoTurma.Bring(c => c.idTurma == idTurma).Select(c => c.Aluno).ToList();
            viewModel.AlunosCadastrados = AlunosCadastrados;
            List<Aluno> AlunosDisponiveis = mAluno.BringAll();
            AlunosDisponiveis.RemoveAll(c => AlunosCadastrados.Exists(x => x.idAluno == c.idAluno));
            viewModel.AlunosDisponiveis = AlunosDisponiveis;
            viewModel.turmaAtual = mTurma.BringOne(c => c.idTurma == idTurma);
            //Teste(viewModel);
            return //PartialView("AdicionarAlunosTurma", viewModel); 
               View("AdicionarAlunosTurma", viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Diretor")]
        public ActionResult AdicionarAlunosTurmaAction(int idTurma)
        {
            var alunos = Request.Form["alunos[]"].Split(',');
            List<Aluno> listAlunos = new List<Aluno>();
            MAluno mAluno = new MAluno();
            Turma turma = new MTurma().BringOne(c => c.idTurma == idTurma);
            MInscricaoTurma mInscricaoTurma = new MInscricaoTurma();
            List<InscricaoTurma> alunosInscritos = mInscricaoTurma.Bring(c => c.idTurma == turma.idTurma);
            List<InscricaoTurma> alunosInscrever = new List<InscricaoTurma>();
            MControleNotas mControleNotas = new MControleNotas();
            foreach (var item in alunos)
            {
                InscricaoTurma novo = new InscricaoTurma();
                novo.idAluno = Convert.ToInt32(item);
                novo.idTurma = turma.idTurma;
                alunosInscrever.Add(novo);
            }
            foreach (var item in alunosInscritos)
            {
                if (alunosInscrever.SingleOrDefault(c => c.idAluno == item.idAluno) == null)
                {
                    mInscricaoTurma.Delete(item);
                }
            }
            foreach (var item in alunosInscrever)
            {
                if (alunosInscritos.SingleOrDefault(c => (c.idAluno == item.idAluno) && (c.idTurma == idTurma)) != null)
                {
                    continue;
                }
                mInscricaoTurma.Add(item);
                foreach (var modulo in turma.Med.Semestre.Modulo)
                {
                    ControleNotas controleNotas = new ControleNotas();
                    controleNotas.idInscricaoTurma = item.idInscricaoTurma;
                    controleNotas.idModulo = modulo.idModulo;
                    mControleNotas.Add(controleNotas);
                }
            }
            return RedirectToAction("GerenciarTurmas", new { idMed = turma.idMed });
        }
        #endregion

        #region Problemas
        [Authorize(Roles = "Diretor")]
        public ActionResult VincularProblemas(int idMed)
        {
            VincularProblemaViewModel dados = new VincularProblemaViewModel();
            dados.MedAtual = new MMed().BringOne(c => c.idMed == idMed);
            dados.ListProblemasCadastrados = new MProblemaXMed().Bring(c => c.idMed == idMed);
            dados.ListProblemaDisponiveis = new MProblemaXMed().RetornaProblemasDisponiveis(dados.MedAtual.idMed);
            return View(dados);
        }

        //[HttpPost]
        //[Authorize(Roles = "Diretor")]
        //public void DeletarVinculoProblema(int idProblema, int idMed)
        //{
        //    MProblemaXMed mProblemaXMed = new MProblemaXMed();
        //    ProblemaXMed cadastroProblemaEncontrado = mProblemaXMed.BringOne(c => (c.idMed == idMed) && (c.idProblema == idProblema));
        //    TempData["Message"] = mProblemaXMed.Delete(cadastroProblemaEncontrado) ? "Vinculo deletado" : "Algo Errado Ocorreu";
        //    GerenciarMedViewModel dados = new GerenciarMedViewModel();
        //    dados.problemasCadastrados = new MProblemaXMed().RetornaProblemasCadastrados(idMed);
        //    dados.turmasCadastradas = new MTurma().Bring(c => c.idMed == idMed);
        //    dados.med = new MMed().BringOne(c => c.idMed == idMed);
        //    //Response.Redirect(Url.Action("GerenciarMed", "ControleMed", idMed));
        //    Response.Redirect(Request.RawUrl);
        //    //Page.Response.Redirect(Page.Request.Url.ToString(), true);
        //    //return RedirectToAction("GerenciarMed", new { id = idMed });
        //}

        [HttpPost]
        [Authorize(Roles = "Diretor")]
        public ActionResult AdicionarProblemas(int idMed)
        {
            var problemas = Request.Form["problemas[]"].Split(',');
            MProblemaXMed mProblemaXMed = new MProblemaXMed();
            List<Problema> listProblemasAdicionar = new List<Problema>();
            List<Problema> listProblemasCadastrados = mProblemaXMed.RetornaProblemasCadastrados(idMed);
            MProblema mProblema = new MProblema();
            foreach (var item in problemas)
            {
                int idProblema = Convert.ToInt32(item);
                Problema problema = mProblema.BringOne(c => c.idProblema == idProblema);
                listProblemasAdicionar.Add(problema);
            }
            foreach (var item in listProblemasCadastrados)
            {
                if (!listProblemasAdicionar.Contains(item))
                {
                    ProblemaXMed problemaXMed = mProblemaXMed.BringOne(c => (c.idMed == idMed) && (c.idProblema == item.idProblema));
                    if (problemaXMed.AvaliacaoTutoria.Any(c => c.FichaAvaliacao.Any(y => y.PerguntaXFicha.Any(z => z.marcado != null))))
                    {
                        continue;
                    }
                    mProblemaXMed.Delete(problemaXMed);
                }
            }
            foreach (var item in listProblemasAdicionar)
            {
                if (listProblemasCadastrados.Contains(item))
                {
                    continue;
                }
                ProblemaXMed novo = new ProblemaXMed();
                novo.idMed = idMed;
                novo.idProblema = item.idProblema;
                mProblemaXMed.Add(novo);
            }
            return RedirectToAction("GerenciarMed", new { id = idMed });
        }
        #endregion

        #region Grupos
        [Authorize(Roles = "Diretor")]
        public ActionResult GerenciarGrupos(int idMed)
        {
            ViewBag.idMed = idMed;
            Med med = new MMed().BringOne(c => c.idMed == idMed);
            ViewBag.idMed = idMed;
            ViewBag.descMedSemestre = med.descMed + " - " + med.Semestre.descSemestre;
            return View(new MGrupo().Bring(c => c.idMed == idMed));
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult AdicionarGrupos(int idMed)
        {
            ViewData["idProfessor"] = new SelectList(new MProfessor().BringAll(), "idProfessor", "nomeProfessor");
            ViewData["idMed"] = idMed;
            return View();
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult AdicionarGrupoAction(Grupo grupo)
        {
            grupo.ativo = true;
            new MGrupo().Add(grupo);
            return RedirectToAction("GerenciarGrupos", "ControleMed", new { idMed = grupo.idMed });
            //Redirect(Url.Action("GerenciarMed", "ControleMed", grupo.idMed));
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult DeletarGrupo(int idGrupo)
        {
            MGrupo mGrupo = new MGrupo();
            Grupo selecionado = mGrupo.BringOne(c => c.idGrupo == idGrupo);
            int idMed = selecionado.idMed;
            TempData["Message"] = mGrupo.Delete(selecionado) ? "Grupo Deletado com Sucesso" : "Algo Errado Ocorreu";
            return RedirectToAction("GerenciarGrupos", "ControleMed", new { id = idMed });
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult AdicionarAlunosGrupo(int idGrupo, int? idInscricaoTurma)
        {
            AlunosGrupoViewModel viewModel = new AlunosGrupoViewModel();
            MGrupo mGrupo = new MGrupo();
            MInscricaoTurma mIncricaoTurma = new MInscricaoTurma();
            viewModel.grupo = mGrupo.BringOne(c => c.idGrupo == idGrupo);
            List<Turma> turmasMed = new MTurma().Bring(c => c.idMed == viewModel.grupo.idMed);
            viewModel.AlunosDisponiveis = new List<InscricaoTurma>();
            foreach (var turma in turmasMed)
            {
                List<InscricaoTurma> alunosTurma = mIncricaoTurma.Bring(c => c.idTurma == turma.idTurma);
                //viewModel.AlunosDisponiveis.AddRange(alunosTurma);
                foreach (var aluno in alunosTurma)
                {
                    viewModel.AlunosDisponiveis.Add(aluno);
                }
            }
            List<Aluno> AlunosInscritos = new List<Aluno>();
            viewModel.AlunosInscritos = mGrupo.BringOne(c => c.idGrupo == idGrupo).InscricaoTurma.ToList();
            viewModel.AlunosDisponiveis.RemoveAll(c => viewModel.AlunosInscritos.Exists(x => c.idAluno == x.idAluno));
            var test = viewModel.grupo.InscricaoTurma;
            foreach (var inscrito in test)
            {
                AlunosInscritos.Add(inscrito.Aluno);
            }

            return View(viewModel);
        }

        public ActionResult VincularAlunosGrupo()
        {
            var keys = Request.Form.AllKeys;
            int idGrupo = Convert.ToInt32(Request.Form["idGrupo"]);
            var alunos = Request.Form["alunos[]"].Split(',');
            MInscricaoTurma mInscricaoTurma = new MInscricaoTurma();
            List<InscricaoTurma> listInscricaoTurma = new List<InscricaoTurma>();
            foreach (var aluno in alunos)
            {
                int idInscricaoTurma = Convert.ToInt32(aluno);
                listInscricaoTurma.Add(mInscricaoTurma.BringOne(c => c.idInscricaoTurma == idInscricaoTurma));
            }
            MGrupo mGrupo = new MGrupo();
            Grupo grupo = mGrupo.BringOne(c => c.idGrupo == idGrupo);
            int[] idsAlunosRemover = grupo.InscricaoTurma.Where(c => !listInscricaoTurma.Exists(x => x.idInscricaoTurma == c.idInscricaoTurma)).Select(c => c.idInscricaoTurma).ToArray();
            List<InscricaoTurma> listAlunosRemover = new List<InscricaoTurma>();
            foreach (var aluno in idsAlunosRemover)
            {
                int idInscricaoTurma = Convert.ToInt32(aluno);
                InscricaoTurma alunoRemover = mInscricaoTurma.BringOne(c => c.idInscricaoTurma == idInscricaoTurma);
                listAlunosRemover.Add(alunoRemover);
            }
            listInscricaoTurma = listInscricaoTurma.Where(c => !grupo.InscricaoTurma.ToList().Exists(x => x.idInscricaoTurma == c.idInscricaoTurma)).ToList();
            List<InscricaoTurma> listAlunosVinculados = grupo.InscricaoTurma.ToList();
            mGrupo.DesvincularAlunoGrupo(grupo.idGrupo, listAlunosRemover.Select(c => c.idInscricaoTurma).ToArray());
            mGrupo.AdicionarAlunoGrupo(grupo.idGrupo, listInscricaoTurma.Select(c => c.idInscricaoTurma).ToArray());
            return RedirectToAction("GerenciarGrupos", new { idMed = grupo.idMed });
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult RemoverAlunosGrupo(int idGrupo, int idInscricaoTurma)
        {
            new MInscricaoTurmaXGrupo().Remove(idGrupo, idInscricaoTurma);
            return RedirectToAction("AdicionarAlunosGrupo", "ControleMed", new { idGrupo = idGrupo });
        }
        #endregion

        #region Simulados
        [Authorize(Roles = "Diretor")]
        public ActionResult GerenciarSimulados(int idMed)
        {
            Med med = new MMed().BringOne(c => c.idMed == idMed);
            ViewBag.idMed = idMed;
            ViewBag.descMedSemestre = med.descMed + " - " + med.Semestre.descSemestre;
            List<Prova> simulados = med.Prova.ToList();
            if (simulados == null)
            {
                simulados = new List<Prova>();
                return View(simulados);
            }
            else
            {
                return View(simulados);
            }
            

        }

        [HttpGet, Authorize(Roles = "Diretor")]
        public ActionResult AdicionarSimulado(int idMed)
        {
            Med med = new MMed().BringOne(c => c.idMed == idMed);
            ViewBag.idTipoProva = new SelectList(new MTipoProva().BringAll(), "idTipoProva", "descTipoProva");
            ViewBag.idModulo = new SelectList(med.Semestre.Modulo, "idModulo", "descModulo");
            ViewBag.idMed = idMed;
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "Diretor")]
        public ActionResult AdicionarSimulado(Prova prova)
        {
            MProva mProva = new MProva();
            if (mProva.Add(prova))
            {
                ViewBag.Message = "Simulado inserido";
                return RedirectToAction("GerenciarSimulados", new { idMed = prova.idMed });
            }
            else
            {
                ViewBag.Message = "Simulado não inserido";
                return RedirectToAction("AdicionarSimulado", new { idMed = prova.idMed });
            }
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult AvaliarSimulado(int idProva)
        {
            Prova prova = new MProva().BringOne(c => c.idProva == idProva);
            Med med = new MMed().BringOne(c => c.idMed == prova.idMed);
            List<Turma> turmas = med.Turma.ToList();
            List<ControleNotasXProva> listAvaliacoes = new List<ControleNotasXProva>();
            MControleNotasXProva mControleNotasXProva = new MControleNotasXProva();
            foreach (Turma turma in turmas)
            {
                foreach (InscricaoTurma alunoInscrito in turma.InscricaoTurma)
                {
                    ControleNotas controleNotas = alunoInscrito.ControleNotas.SingleOrDefault(c => c.idModulo == prova.idModulo);
                    ControleNotasXProva controleNotasXProva = controleNotas.ControleNotasXProva.SingleOrDefault(c => c.idProva == prova.idProva);
                    if (controleNotasXProva == null)
                    {
                        controleNotasXProva = new ControleNotasXProva()
                        {
                            idControleNotas = controleNotas.idControleNotas,
                            idProva = prova.idProva
                        };
                        mControleNotasXProva.Add(controleNotasXProva);
                    }
                    controleNotas = alunoInscrito.ControleNotas.SingleOrDefault(c => c.idModulo == prova.idModulo);
                    controleNotasXProva = controleNotas.ControleNotasXProva.SingleOrDefault(c => c.idProva == prova.idProva);
                    listAvaliacoes.Add(controleNotasXProva);
                }
            }
            return View(listAvaliacoes);
        }

        [Authorize]
        public ActionResult InserirNotasSimulado(int[] idControleNotas, int[] numeroAcertos, int idProva)
        {
            MControleNotasXProva mControleNotasXProva = new MControleNotasXProva();
            for (int i = 0; i < idControleNotas.Length; i++)
            {
                var item = idControleNotas[i];
                ControleNotasXProva controleNotasXProva = mControleNotasXProva.BringOne(c => (c.idControleNotas == item) && (c.idProva == idProva));
                controleNotasXProva.numAcertos = numeroAcertos[i];
                mControleNotasXProva.Update(controleNotasXProva);
            }
            Prova prova = new MProva().BringOne(c => c.idProva == idProva);
            return RedirectToAction("GerenciarSimulados", new { idMed = prova.idMed });
        }

        #endregion

        #region Medias
        [Authorize(Roles = "Diretor")]
        public ActionResult GerenciarNotas(int idMed)
        {
            Med med = new MMed().BringOne(c => c.idMed == idMed);
            ViewBag.idMed = idMed;
            ViewBag.descMedSemestre = med.descMed + " - " + med.Semestre.descSemestre;
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
            return View(listGerenciaNotas);
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult AdicionarNotaRecuperacao(int idInscricaoTurma, double notaRecuperacao)
        {
            return null;
        }

        #endregion

    }
}