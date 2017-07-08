using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MInscricaoTurma : IFunctions<InscricaoTurma>
    {

        private FamervEntities db;

        public MInscricaoTurma()
        {
            db = Singletone.InstanceFamerv;           
        }

        public bool Add(InscricaoTurma t)
        {
            try
            {
                db.InscricaoTurma.Add(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public List<InscricaoTurma> Bring(Expression<Func<InscricaoTurma, bool>> predicate)
        {
            return db.InscricaoTurma.Where(predicate).ToList();
        }

        public List<InscricaoTurma> BringAll()
        {
            return db.InscricaoTurma.ToList();
        }

        public InscricaoTurma BringOne(Expression<Func<InscricaoTurma, bool>> predicate)
        {
            return db.InscricaoTurma.Where(predicate).FirstOrDefault();
        }

        public bool Delete(InscricaoTurma t)
        {
            try
            {
                db.InscricaoTurma.Remove(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public bool Update(InscricaoTurma t)
        {
            try
            {
                InscricaoTurma insc = db.InscricaoTurma.Where(c => c.idInscricaoTurma == t.idInscricaoTurma).FirstOrDefault();
                insc.ativo = t.ativo;
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