using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MUsuario : IFunctions<Usuario>
    {

        private FamervEntities db;

        public MUsuario()
        {
            db = Singletone.InstanceFamerv;
        }

        public bool Add(Usuario t)
        {
            try
            {
                db.Usuario.Add(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public List<Usuario> Bring(Expression<Func<Usuario, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> BringAll()
        {
            throw new NotImplementedException();
        }

        public Usuario BringOne(Expression<Func<Usuario, bool>> predicate)
        {
            return db.Usuario.SingleOrDefault(predicate);
        }

        public bool Delete(Usuario t)
        {
            try
            {
                db.Usuario.Remove(t);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public bool Update(Usuario t)
        {
            try
            {
                Usuario user = db.Usuario.SingleOrDefault(c => c.idUsuario == t.idUsuario);
                user.login = t.login;
                user.senha = t.senha;
                user.idTipoUsuario = t.idTipoUsuario;
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