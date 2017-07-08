using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MAvaliacaoTutoria : IFunctions<AvaliacaoTutoria>
    {

        private FamervEntities db;

        public MAvaliacaoTutoria()
        {
            db = Singletone.InstanceFamerv;
        }

        public bool Add(AvaliacaoTutoria t)
        {
            try
            {
                db.AvaliacaoTutoria.Add(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public List<AvaliacaoTutoria> Bring(Expression<Func<AvaliacaoTutoria, bool>> predicate)
        {
            return db.AvaliacaoTutoria.Where(predicate).ToList();
        }

        public List<AvaliacaoTutoria> BringAll()
        {
            return db.AvaliacaoTutoria.ToList();
        }

        public AvaliacaoTutoria BringOne(Expression<Func<AvaliacaoTutoria, bool>> predicate)
        {
            return db.AvaliacaoTutoria.Where(predicate).FirstOrDefault();
        }

        public bool Delete(AvaliacaoTutoria t)
        {
            try
            {
                db.AvaliacaoTutoria.Remove(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public bool Update(AvaliacaoTutoria t)
        {
            try
            {
                AvaliacaoTutoria avaliacao = db.AvaliacaoTutoria.SingleOrDefault(c => c.idAvaliacaoTutoria == t.idAvaliacaoTutoria);
                avaliacao.dtFim = t.dtFim;
                avaliacao.dtInicio = t.dtInicio;
                avaliacao.notaProfessor = t.notaProfessor;
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