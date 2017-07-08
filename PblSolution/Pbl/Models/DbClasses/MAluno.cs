using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MAluno : IFunctions<Aluno>
    {
        private FamervEntities db;

        public MAluno()
        {
            db = Singletone.InstanceFamerv;
        }

        public bool Add(Aluno t)
        {
            try
            {
                db.Aluno.Add(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public List<Aluno> Bring(Expression<Func<Aluno, bool>> predicate)
        {
            return db.Aluno.Where(predicate).ToList();
        }

        public List<Aluno> BringAll()
        {
            return db.Aluno.ToList();
        }

        public Aluno BringOne(Expression<Func<Aluno, bool>> predicate)
        {
            return db.Aluno.Where(predicate).FirstOrDefault();
        }

        public bool Delete(Aluno T)
        {
            try
            {
                db.Aluno.Remove(T);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public bool Update(Aluno t)
        {
            try
            {
                Aluno alterar = this.BringOne(c => c.idAluno == t.idAluno);
                alterar.cpfAluno = t.cpfAluno;
                alterar.nomeAluno = t.nomeAluno;
                alterar.matriculaAluno = t.matriculaAluno;
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }
    }
}