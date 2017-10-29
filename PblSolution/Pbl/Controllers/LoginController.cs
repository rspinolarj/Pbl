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
            if (string.IsNullOrEmpty(user.email))
            {
                RedirectToAction("Perfil");
            }
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
                foreach (var item in usuario.Aluno)
                {
                    viewModel.nome = item.nomeAluno;
                }
                //viewModel.nome = usuario.Aluno.GetEnumerator().Current.nomeAluno;
            }
            else
            {
                foreach (var item in usuario.Professor)
                {
                    viewModel.nome = item.nomeProfessor;
                }
                //.GetEnumerator().Current.nomeProfessor;
            }
            return View(viewModel);
        }

        [Authorize(Roles = "Diretor,Professor,Aluno")]
        public ActionResult AtualizarInformacoes(string email)
        {
            return null;
        }

        [Authorize(Roles = "Diretor,Professor,Aluno")]
        public ActionResult AlterarSenha(string senha)
        {
            return null;
        }

        public ActionResult RecuperarSenha()
        {
            return View();
        }
    }
}