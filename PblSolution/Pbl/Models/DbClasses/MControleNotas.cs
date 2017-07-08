using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MControleNotas : IFunctions<ControleNotas>
    {

        private FamervEntities db;

        public MControleNotas()
        {
            db = Singletone.InstanceFamerv;
        }

        public bool Add(ControleNotas t)
        {
            try
            {
                db.ControleNotas.Add(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public List<ControleNotas> Bring(Expression<Func<ControleNotas, bool>> predicate)
        {
            return db.ControleNotas.Where(predicate).ToList();
        }

        public List<ControleNotas> BringAll()
        {
            return db.ControleNotas.ToList();
        }

        public ControleNotas BringOne(Expression<Func<ControleNotas, bool>> predicate)
        {
            return db.ControleNotas.Where(predicate).FirstOrDefault();
        }

        public bool Delete(ControleNotas t)
        {
            try
            {
                db.ControleNotas.Remove(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public bool Update(ControleNotas t)
        {
            throw new NotImplementedException();
        }
    }
}