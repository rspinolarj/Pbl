using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MMed : IFunctions<Med>
    {

        private FamervEntities db;

        public MMed()
        {
            db = Singletone.InstanceFamerv;
        }

        public bool Add(Med t)
        {
            throw new NotImplementedException();
        }

        public List<Med> Bring(Expression<Func<Med, bool>> predicate)
        {
            return db.Med.Where(predicate).ToList();
        }

        public List<Med> BringAll()
        {
            return db.Med.ToList();
        }

        public Med BringOne(Expression<Func<Med, bool>> predicate)
        {
            return db.Med.Where(predicate).FirstOrDefault();
        }

        public bool Delete(Med T)
        {
            throw new NotImplementedException();
        }

        public bool Update(Med t)
        {
            throw new NotImplementedException();
        }

        public void AdicionarDisciplinas(int idMed, List<Disciplina> disciplinas)
        {
            Med med = db.Med.SingleOrDefault(c => c.idMed == idMed);
            foreach (var disciplina in disciplinas)
            {
                med.Disciplina.Add(disciplina);
            }
            db.SaveChanges();
        }

        public void DesvincularDisciplinas(int idMed, List<Disciplina> disciplinas)
        {
            Med med = db.Med.SingleOrDefault(c => c.idMed == idMed);
            foreach (var disciplina in disciplinas)
            {
                med.Disciplina.Remove(disciplina);
            }
            db.SaveChanges();
        }
    }
}