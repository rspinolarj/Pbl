using System;
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
            db = Singletone.InstanceFamerv;
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

        private decimal RetornaNota(int idControleNotas)
        {
            var controleNotas = db.ControleNotas.SingleOrDefault(c => c.idControleNotas == idControleNotas);
            var provasMorfofuncionais = controleNotas.ControleNotasXProva.Where(c => c.Prova.idTipoProva == (int)Enumeradores.TipoProva.Morfofuncional).ToList();
            var provasTutoria = controleNotas.ControleNotasXProva.Where(c => c.Prova.idTipoProva == (int)Enumeradores.TipoProva.Tutoria).ToList();
            var disciplinasMorfofuncionais = controleNotas.ControleNotasXAula.Where(c => c.Aula.Disciplina.idTipoDisciplina == (int)Enumeradores.TipoDisciplina.Morfofuncional).ToList();
            var disciplinasPraticas = controleNotas.ControleNotasXAula.Where(c => c.Aula.Disciplina.idTipoDisciplina == (int)Enumeradores.TipoDisciplina.Pratica).ToList();
            List<decimal> notaProblema = new List<decimal>();
            foreach (var avaliacaoTutoria in controleNotas.AvaliacaoTutoria)
            {
                decimal notaAuto = 0;
                decimal notaProfessor = 0;
                decimal notaInterpartes = 0;
                foreach (var fichaAvaliacao in avaliacaoTutoria.FichaAvaliacao)
                {
                    if (fichaAvaliacao.Usuario.Aluno.Count > 0)
                    {
                        Aluno aluno = fichaAvaliacao.Usuario.Aluno.FirstOrDefault();
                        if (aluno.idAluno == avaliacaoTutoria.ControleNotas.InscricaoTurma.idAluno)
                        {
                            //notaAuto = 
                        }
                        else
                        {

                        }
                    }
                    else if (fichaAvaliacao.Usuario.Professor.Count > 0)
                    {
                        Professor professor = fichaAvaliacao.Usuario.Professor.FirstOrDefault();
                    }
                }
            }
            var notaMorfofuncional = decimal.Zero;
            var notaTutoria = decimal.Zero;
            var notaFinal = decimal.Zero;
            return notaFinal;
        }
    }
}