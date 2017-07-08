using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MModulo : IFunctions<Modulo>
    {

        private FamervEntities db;

        public MModulo()
        {
            db = Singletone.InstanceFamerv;
        }

        public bool Add(Modulo t)
        {
            throw new NotImplementedException();
        }

        public List<Modulo> Bring(Expression<Func<Modulo, bool>> predicate)
        {
            return db.Modulo.Where(predicate).ToList();
        }

        public List<Modulo> BringAll()
        {
            return db.Modulo.ToList();
        }

        public Modulo BringOne(Expression<Func<Modulo, bool>> predicate)
        {
            return db.Modulo.Where(predicate).FirstOrDefault();
        }

        public bool Delete(Modulo t)
        {
            throw new NotImplementedException();
        }

        public bool Update(Modulo t)
        {
            throw new NotImplementedException();
        }
    }
}