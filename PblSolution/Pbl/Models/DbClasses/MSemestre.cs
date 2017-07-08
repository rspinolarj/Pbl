using Pbl.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MSemestre : IFunctions<Semestre>
    {
        private FamervEntities db;

        public MSemestre()
        {
            db = Singletone.InstanceFamerv;
        }

        public bool Add(Pbl.Models.ViewModel.SemestreViewModel t)
        {
            try
            {
                //db.SpCrudSemestre(null, t.DataInicioModulo1.Year, t.Descricao, t.DataInicioModulo1, t.DataInicioModulo2, t.DataInicioModulo3, t.DataFinalModulo1, t.DataFinalModulo2, t.DataFinalModulo3, "Insert");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public bool Add(Semestre t)
        {
            throw new NotImplementedException();
        }

        public List<Semestre> Bring(Expression<Func<Semestre, bool>> predicate)
        {
            return db.Semestre.Where(predicate).ToList();
        }

        public List<Semestre> BringAll()
        {
            return db.Semestre.ToList();
        }

        public Semestre BringOne(Expression<Func<Semestre, bool>> predicate)
        {
            return db.Semestre.Where(predicate).FirstOrDefault();
        }

        public bool Delete(Semestre T)
        {
            try
            {
                //db.SpCrudSemestre(T.idSemestre, null, null, null, null, null, null, null, null, "Inativa");
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public bool Update(SemestreViewModel t)
        {
            try
            {
                //db.SpCrudSemestre(t.idSemestre, t.Ano.Year, t.Descricao, t.DataInicioModulo1, t.DataInicioModulo2, t.DataInicioModulo3, t.DataFinalModulo1, t.DataFinalModulo2, t.DataFinalModulo3, "Update");
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public bool Update(Semestre t)
        {
            throw new NotImplementedException();
        }
    }
}