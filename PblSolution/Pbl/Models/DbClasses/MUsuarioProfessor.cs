using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MUsuarioProfessor
    {

        private FamervEntities db;

        public MUsuarioProfessor()
        {
            db = Singletone.InstanceFamerv;
        }

        public bool Add(int idUsuario, int idProfessor)
        {
            try
            {
                db.Usuario.SingleOrDefault(c => c.idUsuario == idUsuario).Professor.Add(db.Professor.SingleOrDefault(h => h.idProfessor == idProfessor));
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

        public bool Remove(int idUsuario, int idProfessor)
        {
            try
            {
                db.Usuario.SingleOrDefault(c => c.idUsuario == idUsuario).Professor.Remove(db.Professor.SingleOrDefault(h => h.idProfessor == idProfessor));
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