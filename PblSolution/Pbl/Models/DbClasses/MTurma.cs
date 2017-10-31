using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MTurma : IFunctions<Turma>
    {
        private FamervEntities db;

        public MTurma()
        {
            db = new FamervEntities();
        }

        public bool Add(Turma t)
        {
            try
            {
                //db.SpCrudTurma(null,t.descTurma,t.id)
                db.Turma.Add(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;

        }

        public List<Turma> Bring(Expression<Func<Turma, bool>> predicate)
        {
            return db.Turma.Where(predicate).ToList();
        }

        public List<Turma> BringAll()
        {
            return db.Turma.ToList();
        }

        public Turma BringOne(Expression<Func<Turma, bool>> predicate)
        {
            return db.Turma.Where(predicate).FirstOrDefault();
        }

        public bool Delete(Turma T)
        {
            try
            {
                db.Turma.Remove(T);
                db.SaveChanges();
                //db.SpCrudTurma(T.idTurma, null, null, null, null, "Inativa");
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

        public bool Update(Turma t)
        {
            try
            {
                Turma turma = db.Turma.SingleOrDefault(c => c.idTurma == t.idTurma);
                turma.ativo = t.ativo;
                turma.descTurma = t.descTurma;
                db.SaveChanges();
                //db.SpCrudTurma(t.idTurma, null, null, null, null, "Update");
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