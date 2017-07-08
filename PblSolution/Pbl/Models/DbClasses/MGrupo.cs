using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MGrupo : IFunctions<Grupo>
    {

        private FamervEntities db;

        public MGrupo()
        {
            db = Singletone.InstanceFamerv;
        }

        public bool Add(Grupo t)
        {
            try
            {
                db.Grupo.Add(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public List<Grupo> Bring(Expression<Func<Grupo, bool>> predicate)
        {
            return db.Grupo.Where(predicate).ToList();
        }

        public List<Grupo> BringAll()
        {
            return db.Grupo.ToList();
        }

        public Grupo BringOne(Expression<Func<Grupo, bool>> predicate)
        {
            return db.Grupo.Where(predicate).FirstOrDefault();
        }

        public bool Delete(Grupo t)
        {
            try
            {
                db.Grupo.Remove(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public bool Update(Grupo t)
        {
            try
            {
                Grupo grupo = db.Grupo.Where(c => c.idGrupo == t.idGrupo).FirstOrDefault();
                grupo.ativo = t.ativo;
                grupo.descGrupo = t.descGrupo;
                grupo.idProfessor = t.idProfessor;
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