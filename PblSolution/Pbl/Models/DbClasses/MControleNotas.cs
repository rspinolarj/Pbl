﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MControleNotas : IFunctions<ControleNotas>
    {

        private FamervEntities db;

        public MControleNotas()
        {
            db = new FamervEntities();
        }

        public bool Add(ControleNotas t)
        {
            try
            {
                db.ControleNotas.Add(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public List<ControleNotas> Bring(Expression<Func<ControleNotas, bool>> predicate)
        {
            return db.ControleNotas.Where(predicate).ToList();
        }

        public List<ControleNotas> BringAll()
        {
            return db.ControleNotas.ToList();
        }

        public ControleNotas BringOne(Expression<Func<ControleNotas, bool>> predicate)
        {
            return db.ControleNotas.Where(predicate).FirstOrDefault();
        }

        public bool Delete(ControleNotas t)
        {
            try
            {
                db.ControleNotas.Remove(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public bool Update(ControleNotas t)
        {
            throw new NotImplementedException();
        }

        public double RetornaNota(int idControleNotas)
        {
            try
            {
                var controleNotas = db.ControleNotas.SingleOrDefault(c => c.idControleNotas == idControleNotas);
                var provaMorfofuncional = controleNotas.ControleNotasXProva.SingleOrDefault(c => c.Prova.idTipoProva == (int)Enumeradores.TipoProva.Morfofuncional);
                var provaTutoria = controleNotas.ControleNotasXProva.SingleOrDefault(c => c.Prova.idTipoProva == (int)Enumeradores.TipoProva.Tutoria);
                var disciplinasFormativas = controleNotas.ControleNotasXAula.Where(c => c.Aula.Disciplina.idTipoDisciplina == (int)Enumeradores.TipoDisciplina.Formativas).ToList();
                var disciplinasPraticas = controleNotas.ControleNotasXAula.Where(c => c.Aula.Disciplina.idTipoDisciplina == (int)Enumeradores.TipoDisciplina.Pratica).ToList();
                double notaProblema = 0;
                foreach (var avaliacaoTutoria in controleNotas.AvaliacaoTutoria)
                {
                    double notaAuto = 0;
                    double notaProfessor = 0;
                    double notaInterpartes = 0;
                    foreach (var fichaAvaliacao in avaliacaoTutoria.FichaAvaliacao)
                    {
                        if (fichaAvaliacao.Usuario.Aluno.Count > 0)
                        {
                            Aluno aluno = fichaAvaliacao.Usuario.Aluno.FirstOrDefault();
                            if (aluno.idAluno == avaliacaoTutoria.ControleNotas.InscricaoTurma.idAluno)
                            {
                                notaAuto = fichaAvaliacao.PerguntaXFicha.Count(c => c.marcado == true);
                            }
                            else
                            {
                                notaInterpartes = fichaAvaliacao.PerguntaXFicha.Count(c => c.marcado == true);
                            }
                        }
                        else if (fichaAvaliacao.Usuario.Professor.Count > 0)
                        {
                            Professor professor = fichaAvaliacao.Usuario.Professor.FirstOrDefault();
                            notaProfessor = fichaAvaliacao.PerguntaXFicha.Count(c => c.marcado == true);
                        }
                    }
                    notaProblema += (((notaProfessor * 0.2) + (notaAuto * 0.05) + ((notaInterpartes / avaliacaoTutoria.FichaAvaliacao.Count - 2) * 0.05)) / 0.9);
                }
                var notaTutoria = (notaProblema / controleNotas.AvaliacaoTutoria.Count()) + ((provaTutoria.Prova.numeroQuestoes / 70) * provaTutoria.numAcertos.Value);
                var notaDisciplinasPraticas = disciplinasPraticas.Sum(c => c.nota.Value) / disciplinasPraticas.Count();
                var notaDisciplinasFormativas = disciplinasFormativas.Sum(c => c.nota.Value) / disciplinasFormativas.Count();
                var notaMorfofuncional = (((double)notaDisciplinasPraticas * 0.3) + ((double)notaDisciplinasFormativas * 0.2) + ((provaMorfofuncional.Prova.numeroQuestoes / 50) * provaMorfofuncional.numAcertos.Value));
                var notaFinal = ((notaTutoria * 60) + (notaMorfofuncional * 0.4));
                if (double.IsNaN(notaFinal))
                {
                    return 0;
                }
                return notaFinal;
            }
            catch (Exception Ex)
            {
                return 0;
            }
        }

        public double retornaNotaProblema(int idAvaliacaoTutoria)
        {
            AvaliacaoTutoria avaliacaoTutoria = db.AvaliacaoTutoria.SingleOrDefault(c => c.idAvaliacaoTutoria == idAvaliacaoTutoria);
            double notaAuto = 0;
            double notaProfessor = 0;
            double notaInterpartes = 0;
            int numeroInterpares = 0;
            foreach (var fichaAvaliacao in avaliacaoTutoria.FichaAvaliacao)
            {
                if (fichaAvaliacao.Usuario.Aluno.Count > 0)
                {
                    Aluno aluno = fichaAvaliacao.Usuario.Aluno.FirstOrDefault();
                    if (aluno.idAluno == avaliacaoTutoria.ControleNotas.InscricaoTurma.idAluno)
                    {
                        notaAuto = fichaAvaliacao.PerguntaXFicha.Count(c => c.marcado == true);
                    }
                    else
                    {
                        notaInterpartes = fichaAvaliacao.PerguntaXFicha.Count(c => c.marcado == true);
                        numeroInterpares++;
                    }
                }
                else if (fichaAvaliacao.Usuario.Professor.Count > 0)
                {
                    Professor professor = fichaAvaliacao.Usuario.Professor.FirstOrDefault();
                    notaProfessor = fichaAvaliacao.PerguntaXFicha.Count(c => c.marcado == true);
                }
            }
            notaProfessor = notaProfessor * 0.2;
            notaAuto = notaAuto * 0.05;
            if (numeroInterpares != 0)
            {
                notaInterpartes = (notaInterpartes / numeroInterpares) * 0.05;
            }
            var notaProblema = ((notaProfessor + notaAuto + notaInterpartes) / 0.9)*10;
            if (double.IsNaN(notaProblema))
            {
                notaProblema = 0;
            }
            return notaProblema;
        }

        public double retornaNotaDisciplina(int idAula, int idControleNotas)
        {
            var nota = db.ControleNotasXAula.SingleOrDefault(c => c.idAula == idAula && c.idControleNotas == idControleNotas).nota;
            return nota.HasValue ? (double)nota.Value : 0;
        }

        public double retornaNotaSimulado(int idControleNotas, int idProva)
        {
            var controleNotasXProva = db.ControleNotasXProva.SingleOrDefault(c => c.idProva == idProva && c.idControleNotas == idControleNotas);
            double nota = 0;
            try
            {
                if (controleNotasXProva.numAcertos.HasValue)
                {
                    nota = (70 / (double)controleNotasXProva.Prova.numeroQuestoes) * controleNotasXProva.numAcertos.Value;
                }
            }
            catch (Exception Ex)
            {

            }
            
            return nota;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}