using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MControleNotasXAula : IFunctions<ControleNotasXAula>
    {

        private FamervEntities db;

        public MControleNotasXAula()
        {
            db = Singletone.InstanceFamerv;
        }

        public bool Add(ControleNotasXAula t)
        {
            try
            {
                db.ControleNotasXAula.Add(t);
                db.SaveChanges();
                db = Singletone.Refresh;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public List<ControleNotasXAula> Bring(Expression<Func<ControleNotasXAula, bool>> predicate)
        {
            return db.ControleNotasXAula.Where(predicate).ToList();
        }

        public List<ControleNotasXAula> BringAll()
        {
            return db.ControleNotasXAula.ToList();
        }

        public ControleNotasXAula BringOne(Expression<Func<ControleNotasXAula, bool>> predicate)
        {
            return db.ControleNotasXAula.Where(predicate).FirstOrDefault();
        }

        public bool Delete(ControleNotasXAula t)
        {
            try
            {
                db.ControleNotasXAula.Remove(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public bool Update(ControleNotasXAula t)
        {
            try
            {
                ControleNotasXAula controleNotasAula = db.ControleNotasXAula.Where(c => (c.idControleNotas == t.idControleNotas) && (c.idAula == t.idAula)).FirstOrDefault();
                controleNotasAula.nota = t.nota;
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