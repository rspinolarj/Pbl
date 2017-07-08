using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MProfessor : IFunctions<Professor>
    {
        private FamervEntities db;

        public MProfessor()
        {
            db = Singletone.InstanceFamerv;
        }

        public bool Add(Professor t)
        {
            try
            {
                db.Professor.Add(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public List<Professor> Bring(Expression<Func<Professor, bool>> predicate)
        {
            return db.Professor.Where(predicate).ToList();
        }

        public List<Professor> BringAll()
        {
            return db.Professor.ToList();
        }

        public Professor BringOne(Expression<Func<Professor, bool>> predicate)
        {
            return db.Professor.Where(predicate).FirstOrDefault();
        }

        public bool Delete(Professor T)
        {
            try
            {
                db.Professor.Remove(T);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public bool Update(Professor t)
        {
            try
            {
                Professor alterar = this.BringOne(c => c.idProfessor == t.idProfessor);
                alterar.cpfProfessor = t.cpfProfessor;
                alterar.nomeProfessor = t.nomeProfessor;
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