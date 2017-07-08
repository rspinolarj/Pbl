using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pbl.Models;

namespace Pbl.Controllers
{
    public class UsuariosController : Controller
    {
        private FamervEntities db = new FamervEntities();

        // GET: Usuarios/Details/5
        [Authorize]
        public ActionResult Details()
        {
            int id = ((Usuario)Session["Usuario"]).idUsuario;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTipoUsuario = new SelectList(db.TipoUsuario, "idTipoUsuario", "descricao", usuario.idTipoUsuario);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUsuario,login,senha,idTipoUsuario")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTipoUsuario = new SelectList(db.TipoUsuario, "idTipoUsuario", "descricao", usuario.idTipoUsuario);
            return View(usuario);
        }
    }
}
