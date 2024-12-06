using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using web.task.bueno.Common.Base;
using web.task.bueno.Common.Constants;
using web.task.bueno.Models;
using web.task.bueno.presentation.Data.PasswordData;
using web.task.bueno.presentation.Data.UsuarioData;
using web.task.bueno.presentation.Repositories;
using web.task.bueno.presentation.Repositories.Interfaces;

namespace web.task.bueno.Controllers
{
   
    public class HomeController : BaseCustomController
    {
        private IUsuarioRepository usuarioRepository;
        private IToolEncryptionRepository toolRepository;
        private bool validarCaptcha = false;
        
        public HomeController()
        {
            usuarioRepository = new UsuarioRepository(getStringConnection());
            toolRepository = new ToolEncryptionRepository();
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
        public ActionResult Create()
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
        public async Task<ActionResult> Crear(FormCollection formCollection)
        {

            try
            {
                string nombre = Convert.ToString(formCollection["nombre"]);
                string correo = Convert.ToString(formCollection["correo"]);
                string clave = Convert.ToString(formCollection["clave"]);
                string captcha = Convert.ToString(formCollection["captcha"]);

                string captchaGenerate = getLetterCaptcha();

                var hashed = toolRepository.EncodePassword(clave);

                if (validarCaptcha == true && captchaGenerate != captcha)
                {
                    stateViewTransaction = MessageConstants.ERROR_CAPTCHA;
                    return RedirectToAction("Create", "Home");
                }

                var crear = new UsuarioRequest
                {
                    Nombre = nombre,
                    Correo = correo,
                    Clave = hashed,
                    Salt = ""
                };

                var usuario = await usuarioRepository.CrearUsuario(crear);

                if (usuario.Id == 0) {
                    stateViewTransaction = MessageConstants.ERROR_GENERICO_REGISTRO;
                    return RedirectToAction("Create", "Home");
                }

                stateViewTransaction = MessageConstants.EXITO_USUARIO_REGISTRO;

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                stateViewTransaction = MessageConstants.ERROR_GENERICO + " " + ex.Message;
                return RedirectToAction("Create", "Home");
            }
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

                if (validarCaptcha == true && captchaGenerate != captcha.Trim())
                {
                    stateViewTransaction = MessageConstants.ERROR_CAPTCHA;
                    return RedirectToAction("Index", "Home");
                }

                var usuario = await usuarioRepository.Login (correo.Trim());

                if (usuario == null) {
                    stateViewTransaction = MessageConstants.ERROR_LOGIN;
                    return RedirectToAction("Index", "Home");
                }

                var validarPass = toolRepository.ValidatePassword(clave, usuario.Clave);

                if (validarPass == false) {
                    stateViewTransaction = MessageConstants.ERROR_LOGIN_HASHED;
                    return RedirectToAction("Index", "Home");
                }

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
                stateViewTransaction = MessageConstants.ERROR_GENERICO + " " + ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}