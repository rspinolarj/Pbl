using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MUsuarioAluno
    {

        private FamervEntities db;

        public MUsuarioAluno()
        {
            db = new FamervEntities();
        }

        public bool Add(int idUsuario, int idAluno)
        {
            try
            {
                db.Usuario.SingleOrDefault(c => c.idUsuario == idUsuario).Aluno.Add(db.Aluno.SingleOrDefault(h => h.idAluno == idAluno));
                db.SaveChanges();
                Singletone.Refresh();
                db = Singletone.InstanceFamerv;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public bool Remove(int idUsuario, int idAluno)
        {
            try
            {
                db.Usuario.SingleOrDefault(c => c.idUsuario == idUsuario).Aluno.Remove(db.Aluno.SingleOrDefault(h => h.idAluno == idAluno));
                db.SaveChanges();
                Singletone.Refresh();
                db = Singletone.InstanceFamerv;
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