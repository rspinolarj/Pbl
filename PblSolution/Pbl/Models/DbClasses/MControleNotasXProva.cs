using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MControleNotasXProva : IFunctions<ControleNotasXProva>
    {

        private FamervEntities db;

        public MControleNotasXProva()
        {
            db = Singletone.InstanceFamerv;
        }

        public bool Add(ControleNotasXProva t)
        {
            try
            {
                db.ControleNotasXProva.Add(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public List<ControleNotasXProva> Bring(Expression<Func<ControleNotasXProva, bool>> predicate)
        {
            return db.ControleNotasXProva.Where(predicate).ToList();
        }

        public List<ControleNotasXProva> BringAll()
        {
            return db.ControleNotasXProva.ToList();
        }

        public ControleNotasXProva BringOne(Expression<Func<ControleNotasXProva, bool>> predicate)
        {
            return db.ControleNotasXProva.SingleOrDefault(predicate);
        }

        public bool Delete(ControleNotasXProva t)
        {
            try
            {
                db.ControleNotasXProva.Remove(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public bool Update(ControleNotasXProva t)
        {
            try
            {
                ControleNotasXProva controleNotasXProva = db.ControleNotasXProva.SingleOrDefault(c => (c.idControleNotas == t.idControleNotas) && (c.idProva == t.idProva));
                controleNotasXProva.numAcertos = t.numAcertos;
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