using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MProblema : IFunctions<Problema>
    {
        private FamervEntities db;

        public MProblema()
        {
            db = Singletone.InstanceFamerv;
        }

        public bool Add(Problema t)
        {
            try
            {
                db.SpCrudProblema(null, t.tituloProblema, t.descProblema, "Insert");
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public List<Problema> Bring(Expression<Func<Problema, bool>> predicate)
        {
            return db.Problema.Where(predicate).ToList();
        }

        public List<Problema> BringAll()
        {
            return db.Problema.ToList();
        }

        public Problema BringOne(Expression<Func<Problema, bool>> predicate)
        {
            return db.Problema.Where(predicate).FirstOrDefault();
        }

        public bool Delete(Problema t)
        {
            try
            {
                //db.SpCrudProblema(t.idProblema, null, null, "Invalida");
                db.Problema.Remove(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public bool Update(Problema t)
        {
            try
            {
                //db.SpCrudProblema(t.idProblema, t.tituloProblema, t.descProblema, "Update");
                Problema p = db.Problema.Where(c => c.idProblema == t.idProblema).FirstOrDefault();
                p.ativo = t.ativo;
                p.descProblema = t.descProblema;
                p.tituloProblema = t.tituloProblema;
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