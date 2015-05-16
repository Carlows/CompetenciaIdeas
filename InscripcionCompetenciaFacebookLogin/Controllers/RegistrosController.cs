using InscripcionCompetenciaFacebookLogin.Models;
using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace InscripcionCompetenciaFacebookLogin.Controllers
{
    public class RegistrosController : BaseController
    {
        private ApplicationDbContext _context;
        private ApplicationDbContext dbContext
        {
            get
            {
                return _context ?? HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            }
            set
            {
                _context = value;
            }
        }
        
        // GET: Registros
        [Authorize]
        public ActionResult Index()
        {
            if (dbContext.Registros.Count() > 0)
            {
                int maxVotes = dbContext.Registros.Max(r => r.Votos.Count);
                var ideasWinning = dbContext.Registros
                    .Where(r => r.Votos.Count == maxVotes)
                    .Select(r => r.Id)
                    .ToList();

                var model = dbContext.Registros.Select(r => new RegistroViewModel
                    {
                        Id = r.Id,
                        Titulo = r.Titulo,
                        Contenido = r.Contenido,
                        ImgURL = r.ImgUrl,
                        Votos = r.Votos.Count,
                        NombreUsuario = r.Usuario.Nombre,
                        ApellidoUsuario = r.Usuario.Apellido,
                        Winning = ideasWinning.Contains(r.Id)
                    }).ToList();

                return View(model);
            }
            else
            {
                var model = new List<RegistroViewModel>();

                return View(model);
            }
        }

        [Authorize]
        public ActionResult SeleccionarAccion()
        {
            return View();
        }
        
        [Authorize]
        public ActionResult AgregarRegistro()
        {
            var user = UserManager.FindByName(User.Identity.Name);

            if (user.Registro != null)
                return RedirectToAction("Index");

            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarRegistro(RegistroViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.FindByName(User.Identity.Name);

                if (user.Registro != null)
                {
                    ModelState.AddModelError("", "Ya has inscrito una idea a esta competición");
                    return View();
                }

                var nuevoRegistro = new Registro()
                {
                    Titulo = model.Titulo,
                    Contenido = model.Contenido,
                    Usuario = user
                };

                if (model.Imagen != null)
                {
                    string extension = Path.GetExtension(model.Imagen.FileName);               
                    if (extension.Equals(".jpg") || extension.Equals(".jpeg") || extension.Equals(".png"))
                    {
                        string serverPath = Server.MapPath("~/Content");
                        string userCarpeta = String.Format("{0}{1}", user.Nombre, user.Apellido);
                        string dirPath = Path.Combine(serverPath, userCarpeta);

                        if (!Directory.Exists(dirPath))
                            Directory.CreateDirectory(dirPath);

                        string fileName = string.Format("{0:dd-MM-yyyy-HH-mm-ss}{1}", DateTime.Now, extension);
                        string userFolder = Path.Combine(userCarpeta, fileName);
                        string path = Path.Combine(serverPath, userFolder);

                        model.Imagen.SaveAs(path);

                        nuevoRegistro.ImgUrl = userFolder;
                    }
                }

                dbContext.Registros.Add(nuevoRegistro);
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Vote(int id) 
        {
            if (Request.IsAjaxRequest())
            {
                var registro = dbContext.Registros.Find(id);
                var user = UserManager.FindByName(User.Identity.Name);

                bool alreadyVoted = user.Votos.Where(v => v.Registro == registro).SingleOrDefault() != null;
                if (!alreadyVoted)
                {
                    Voto nuevoVoto = new Voto()
                    {
                        Registro = registro,
                        Usuario = user
                    };

                    dbContext.Votos.Add(nuevoVoto);
                    dbContext.SaveChanges();
                }
                else
                {
                    return Json(new { success = false, error = "Ya has votado en esta idea" });
                }

                return Json(new { success = true, votos = registro.Votos.Count });
            }

            return new EmptyResult();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult BorrarRegistro(int id) 
        {
            if (Request.IsAjaxRequest())
            {
                var registro = dbContext.Registros.Find(id);

                if (registro != null)
                {
                    dbContext.Registros.Remove(registro);
                    dbContext.SaveChanges();
                }
            }

            return new EmptyResult();
        }

        [Authorize(Roles="Admin")]
        public ActionResult BorrarTodos()
        {
            return View();
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public ActionResult BorrarTodos(int? id)
        {
            dbContext.Registros.RemoveRange(dbContext.Registros.ToList());
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}