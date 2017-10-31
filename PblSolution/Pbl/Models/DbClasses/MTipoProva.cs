using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MTipoProva : IFunctions<TipoProva>
    {

        private FamervEntities db;

        public MTipoProva()
        {
            db = new FamervEntities();
        }

        public bool Add(TipoProva t)
        {
            try
            {
                db.TipoProva.Add(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public List<TipoProva> Bring(Expression<Func<TipoProva, bool>> predicate)
        {
            return db.TipoProva.Where(predicate).ToList();
        }

        public List<TipoProva> BringAll()
        {
            return db.TipoProva.ToList();
        }

        public TipoProva BringOne(Expression<Func<TipoProva, bool>> predicate)
        {
            return db.TipoProva.SingleOrDefault(predicate);
        }

        public bool Delete(TipoProva t)
        {
            try
            {
                db.TipoProva.Remove(t);
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

        public bool Update(TipoProva t)
        {
            try
            {
                TipoProva tipoProva = db.TipoProva.SingleOrDefault(c => c.idTipoProva == t.idTipoProva);
                tipoProva.pesoProva = t.pesoProva;
                tipoProva.descTipoProva = t.descTipoProva;
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