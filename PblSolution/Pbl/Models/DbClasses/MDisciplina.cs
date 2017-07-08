using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MDisciplina : IFunctions<Disciplina>
    {
        private FamervEntities db;

        public MDisciplina()
        {
            db = Singletone.InstanceFamerv;
        }

        public bool Add(Disciplina t)
        {
            try
            {
                db.Disciplina.Add(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public List<Disciplina> Bring(Expression<Func<Disciplina, bool>> predicate)
        {
            return db.Disciplina.Where(predicate).ToList();
        }

        public List<Disciplina> BringAll()
        {
            return db.Disciplina.ToList();
        }

        public Disciplina BringOne(Expression<Func<Disciplina, bool>> predicate)
        {
            return db.Disciplina.Where(predicate).FirstOrDefault();
        }

        public bool Delete(Disciplina T)
        {
            try
            {
                db.Disciplina.Remove(T);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public bool Update(Disciplina t)
        {
            try
            {
                Disciplina alterar = this.BringOne(c => c.idDisciplina == t.idDisciplina);
                alterar.descDisciplina = t.descDisciplina;
                alterar.idTipoDisciplina = t.idTipoDisciplina;
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