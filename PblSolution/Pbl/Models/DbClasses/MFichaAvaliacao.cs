using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MFichaAvaliacao : IFunctions<FichaAvaliacao>
    {

        private FamervEntities db;

        public MFichaAvaliacao()
        {
            db = new FamervEntities();
        }

        public bool Add(FichaAvaliacao t)
        {
            try
            {
                db.FichaAvaliacao.Add(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public List<FichaAvaliacao> Bring(Expression<Func<FichaAvaliacao, bool>> predicate)
        {
            return db.FichaAvaliacao.Where(predicate).ToList();
        }

        public List<FichaAvaliacao> BringAll()
        {
            return db.FichaAvaliacao.ToList();
        }

        public FichaAvaliacao BringOne(Expression<Func<FichaAvaliacao, bool>> predicate)
        {
            return db.FichaAvaliacao.SingleOrDefault(predicate);
        }

        public bool Delete(FichaAvaliacao t)
        {
            try
            {
                db.FichaAvaliacao.Remove(t);
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

        public bool Update(FichaAvaliacao t)
        {
            try
            {
                FichaAvaliacao fichaAvaliacao = db.FichaAvaliacao.SingleOrDefault(c => c.idFichaAvaliacao == t.idFichaAvaliacao);
                fichaAvaliacao.idAvaliador = t.idAvaliador;
                fichaAvaliacao.idAvaliacaoTutoria = t.idAvaliacaoTutoria;
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