using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MInscricaoTurmaXGrupo
    {

        private FamervEntities db;

        public MInscricaoTurmaXGrupo()
        {
            db = Singletone.InstanceFamerv;
        }

        public bool Add(int idGrupo, int idInscricaoTurma)
        {
            try
            {
                db.Grupo.Where(c => c.idGrupo == idGrupo).First().InscricaoTurma.Add(db.InscricaoTurma.Where(c => c.idInscricaoTurma == idInscricaoTurma).First());
                db.SaveChanges();
                db = Singletone.Refresh;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public bool Remove(int idGrupo, int idInscricaoTurma)
        {
            try
            {
                db.Grupo.Where(c => c.idGrupo == idGrupo).First().InscricaoTurma.Remove(db.InscricaoTurma.Where(c => c.idInscricaoTurma == idInscricaoTurma).First());
                db.SaveChanges();
                db = Singletone.Refresh;
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