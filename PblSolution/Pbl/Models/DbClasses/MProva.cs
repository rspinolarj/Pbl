using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MProva : IFunctions<Prova>
    {

        private FamervEntities db;

        public MProva()
        {
            db = Singletone.InstanceFamerv;
        }

        public bool Add(Prova t)
        {
            try
            {
                db.Prova.Add(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Singletone.Refresh();
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public List<Prova> Bring(Expression<Func<Prova, bool>> predicate)
        {
            return db.Prova.Where(predicate).ToList();
        }

        public List<Prova> BringAll()
        {
            return db.Prova.ToList();
        }

        public Prova BringOne(Expression<Func<Prova, bool>> predicate)
        {
            return db.Prova.SingleOrDefault(predicate);
        }

        public bool Delete(Prova t)
        {
            try
            {
                db.Prova.Remove(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public bool Update(Prova t)
        {
            try
            {
                Prova prova = db.Prova.SingleOrDefault(c => c.idProva == t.idProva);
                prova.numeroQuestoes = t.numeroQuestoes;
                prova.idTipoProva = t.idTipoProva;
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