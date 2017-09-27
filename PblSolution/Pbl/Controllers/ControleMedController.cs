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
            viewModel.disciplinasDisponiveis = new MDisciplina().BringAll();
            viewModel.disciplinasCadastradas = viewModel.med.Disciplina.ToList();
            viewModel.disciplinasDisponiveis.RemoveAll(c => viewModel.disciplinasCadastradas.Contains(c));
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
                listDisciplinasRemover.Add(mdisciplina.BringOne(c => c.idDisciplina == idDisciplina));
            }
            listDisciplinasVincular = listDisciplinasVincular.Where(c => !med.Disciplina.ToList().Exists(x => x.idDisciplina == c.idDisciplina)).ToList();
            List<Disciplina> listDisciplinasVinculadas = med.Disciplina.ToList();
            mMed.DesvincularDisciplinas(med.idMed, listDisciplinasRemover);
            mMed.AdicionarDisciplinas(med.idMed, listDisciplinasVincular);
            return RedirectToAction("GerenciarMed", new { id = idMed });
        }
        #endregion

        #region Turmas
        [Authorize(Roles = "Diretor")]
        public ActionResult GerenciarTurmas(int idMed)
        {
            ViewBag.idMed = idMed;
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
            return RedirectToAction("GerenciarMed", new { id = nova.idMed });
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
            AlunosDisponiveis.RemoveAll(c => AlunosCadastrados.Contains(c));
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
            return AdicionarAlunosTurma(idTurma);
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
            return RedirectToAction("GerenciarMed", "ControleMed", new { id = grupo.idMed });
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
            if (idInscricaoTurma.HasValue)
            {
                new MInscricaoTurmaXGrupo().Add(idGrupo, idInscricaoTurma.Value);
            }
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
            viewModel.AlunosDisponiveis.RemoveAll(c => viewModel.AlunosInscritos.Contains(c));
            var test = viewModel.grupo.InscricaoTurma;
            foreach (var inscrito in test)
            {
                AlunosInscritos.Add(inscrito.Aluno);
            }

            return View(viewModel); //View(viewModel);
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult RemoverAlunosGrupo(int idGrupo, int idInscricaoTurma)
        {
            new MInscricaoTurmaXGrupo().Remove(idGrupo, idInscricaoTurma);
            return RedirectToAction("AdicionarAlunosGrupo", "ControleMed", new { idGrupo = idGrupo });
        }
        #endregion




        [Authorize(Roles = "Diretor")]
        public ActionResult AdicionarAulasTurma(int idTurma)
        {



            return null;
        }
    }
}