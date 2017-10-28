using Pbl.Models;
using Pbl.Models.DbClasses;
using Pbl.Models.ViewModel;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace Pbl.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string login, string senha)
        {
            MUsuario mUser = new MUsuario();
            Usuario user = mUser.BringOne(c => (c.login == login) && (c.senha == senha));
            if (user == null)
            {
                return View();
            }
            FormsAuthentication.SetAuthCookie(user.idUsuario.ToString(), false);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }

        [Authorize(Roles = "Diretor,Professor,Aluno")]
        public ActionResult Perfil()
        {
            PerfilViewModel viewModel = new PerfilViewModel();
            int idUsuario = Convert.ToInt32(User.Identity.Name);
            Usuario usuario = new MUsuario().BringOne(c => c.idUsuario == idUsuario);
            viewModel.usuario = usuario.login;
            viewModel.senha = usuario.senha;
            viewModel.email = usuario.email;
            if (usuario.idTipoUsuario == (int)Enumeradores.TipoUsuario.Aluno)
            {
                viewModel.nome = usuario.Aluno.GetEnumerator().Current.nomeAluno;
            }
            else
            {
                viewModel.nome = usuario.Professor.GetEnumerator().Current.nomeProfessor;
            }
            return View(viewModel);
        }
    }
}