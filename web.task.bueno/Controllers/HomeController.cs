using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using web.task.bueno.Common.Filters;
using web.task.bueno.Common.Session;
using web.task.bueno.Models;
using web.task.bueno.presentation.Repositories;
using web.task.bueno.presentation.Repositories.Interfaces;
using web.task.bueno.Tools;

namespace web.task.bueno.Controllers
{
   
    public class HomeController : Controller
    {
        private IUsuarioRepository usuarioRepository;
        private WebCurrentSession webCurrentSession;
        
        public HomeController()
        {
            usuarioRepository = new UsuarioRepository(Util.getCadenaConexion());
        }

        [HttpGet]
        public ActionResult Index()
        {
            webCurrentSession = new WebCurrentSession(this.HttpContext);
            ViewBag.perfil = webCurrentSession.EsLogeado;
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            webCurrentSession = new WebCurrentSession(this.HttpContext);
            ViewBag.perfil = webCurrentSession.EsLogeado;
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            webCurrentSession = new WebCurrentSession(this.HttpContext);
            webCurrentSession.EsPrimeraVez = "";
            webCurrentSession.EsLogeado = null;
            ViewBag.perfil = null;
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<ActionResult> Login(FormCollection formCollection)
        {
            try
            {
                string correo = Convert.ToString(formCollection["correo"]);
                string clave = Convert.ToString(formCollection["clave"]);

                var usuario = await usuarioRepository.Login (correo, clave);

                if (usuario == null) return RedirectToAction("Index", "Home");


                var datosPerfil = new PerfilUsuario
                {
                    id = usuario.Id,
                    nombreCompleto = usuario.NombreCompleto,
                    correo = correo,
                };

                webCurrentSession = new WebCurrentSession(this.HttpContext);
                webCurrentSession.EsLogeado = datosPerfil;
                ViewBag.perfil = webCurrentSession.EsLogeado;

                return RedirectToAction("Index", "Tarea");

            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return RedirectToAction("Index", "Home");
            }
            
        }

    }
}