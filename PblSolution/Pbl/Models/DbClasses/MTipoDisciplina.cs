using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MTipoDisciplina : IFunctions<TipoDisciplina>
    {
        private FamervEntities db;

        public MTipoDisciplina()
        {
            db = Singletone.InstanceFamerv;
        }

        public bool Add(TipoDisciplina t)
        {
            try
            {
                db.TipoDisciplina.Add(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public List<TipoDisciplina> Bring(Expression<Func<TipoDisciplina, bool>> predicate)
        {
            return db.TipoDisciplina.Where(predicate).ToList();
        }

        public List<TipoDisciplina> BringAll()
        {
            return db.TipoDisciplina.ToList();
        }

        public TipoDisciplina BringOne(Expression<Func<TipoDisciplina, bool>> predicate)
        {
            return db.TipoDisciplina.Where(predicate).FirstOrDefault();
        }

        public bool Delete(TipoDisciplina T)
        {
            try
            {
                db.TipoDisciplina.Remove(T);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public bool Update(TipoDisciplina t)
        {
            try
            {
                TipoDisciplina alterar = this.BringOne(c => c.idTipoDisciplina == t.idTipoDisciplina);
                alterar.descTipoDisciplina = t.descTipoDisciplina;
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