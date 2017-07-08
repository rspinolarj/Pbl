using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MAula : IFunctions<Aula>
    {

        private FamervEntities db;

        public MAula()
        {
            db = Singletone.InstanceFamerv;
        }

        public bool Add(Aula t)
        {
            try
            {
                db.Aula.Add(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public List<Aula> Bring(Expression<Func<Aula, bool>> predicate)
        {
            return db.Aula.Where(predicate).ToList();
        }

        public List<Aula> BringAll()
        {
            return db.Aula.ToList();
        }

        public Aula BringOne(Expression<Func<Aula, bool>> predicate)
        {
            return db.Aula.Where(predicate).FirstOrDefault();
        }

        public bool Delete(Aula t)
        {
            try
            {
                db.Aula.Remove(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public bool Update(Aula t)
        {
            try
            {
                Aula aula = db.Aula.Where(c => c.idAula == t.idAula).FirstOrDefault();
                aula.ativo = t.ativo;
                aula.idProfessor = t.idProfessor;
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