using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using web.task.bueno.Common.Base;
using web.task.bueno.Models;
using web.task.bueno.presentation.Repositories;
using web.task.bueno.presentation.Repositories.Interfaces;

namespace web.task.bueno.Controllers
{
   
    public class HomeController : BaseCustomController
    {
        private IUsuarioRepository usuarioRepository;
        private bool validarCaptcha = false;
        
        public HomeController()
        {
            usuarioRepository = new UsuarioRepository(getStringConnection());
            validarCaptcha = getAllowedCaptcha(); 
        }

        [HttpGet]
        public ActionResult Index()
        {
            getViewSessionPerfil();
            getViewCaptcha(setLetterCaptcha());
            ViewBag.AllowedCaptcha = validarCaptcha;
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            getViewSessionPerfil();
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            getViewSessionPerfil(true);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> Login(FormCollection formCollection)
        {
            try
            {
                string correo = Convert.ToString(formCollection["correo"]);
                string clave = Convert.ToString(formCollection["clave"]);
                string captcha = Convert.ToString(formCollection["captcha"]);

                string captchaGenerate = getLetterCaptcha();

                if (validarCaptcha == true && captchaGenerate != captcha) return RedirectToAction("Index", "Home");

                var usuario = await usuarioRepository.Login (correo, clave);

                if (usuario == null) return RedirectToAction("Index", "Home");

                var datosPerfil = new PerfilUsuario
                {
                    id = usuario.Id,
                    nombreCompleto = usuario.NombreCompleto,
                    correo = correo,
                };

                getViewSession(datosPerfil);

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