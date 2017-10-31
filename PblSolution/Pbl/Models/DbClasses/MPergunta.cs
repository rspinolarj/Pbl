using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MPergunta : IFunctions<Pergunta>
    {

        private FamervEntities db;

        public MPergunta()
        {
            db = new FamervEntities();
        }

        public bool Add(Pergunta t)
        {
            try
            {
                db.Pergunta.Add(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public List<Pergunta> Bring(Expression<Func<Pergunta, bool>> predicate)
        {
            return db.Pergunta.Where(predicate).ToList();
        }

        public List<Pergunta> BringAll()
        {
            return db.Pergunta.ToList();
        }

        public Pergunta BringOne(Expression<Func<Pergunta, bool>> predicate)
        {
            return db.Pergunta.SingleOrDefault(predicate);
        }

        public bool Delete(Pergunta t)
        {
            try
            {
                db.Pergunta.Remove(t);
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

        public bool Update(Pergunta t)
        {
            try
            {
                Pergunta pergunta = db.Pergunta.SingleOrDefault(c => c.idPergunta == t.idPergunta);
                pergunta.pergunta1 = t.pergunta1;
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