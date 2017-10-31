using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MPerguntaXFicha : IFunctions<PerguntaXFicha>
    {

        private FamervEntities db;

        public MPerguntaXFicha()
        {
            db = new FamervEntities();
        }

        public bool Add(PerguntaXFicha t)
        {
            try
            {
                db.PerguntaXFicha.Add(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public List<PerguntaXFicha> Bring(Expression<Func<PerguntaXFicha, bool>> predicate)
        {
            return db.PerguntaXFicha.Where(predicate).ToList();
        }

        public List<PerguntaXFicha> BringAll()
        {
            return db.PerguntaXFicha.ToList();
        }

        public PerguntaXFicha BringOne(Expression<Func<PerguntaXFicha, bool>> predicate)
        {
            return db.PerguntaXFicha.SingleOrDefault(predicate);
        }

        public bool Delete(PerguntaXFicha t)
        {
            try
            {
                db.PerguntaXFicha.Remove(t);
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
            throw new NotImplementedException();
        }

        public bool Update(PerguntaXFicha t)
        {
            try
            {
                PerguntaXFicha perguntaXFicha = db.PerguntaXFicha.SingleOrDefault(c => (c.idFichaAvaliacao == t.idFichaAvaliacao) && (c.idPergunta == t.idPergunta));
                perguntaXFicha.marcado = t.marcado;
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