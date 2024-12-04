using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.task.bueno.Common.Session;
using web.task.bueno.Models;
using web.task.bueno.Tools;

namespace web.task.bueno.Common.Base
{
    public abstract class BaseCustomController : Controller
    {
        //get perfil session
        private static PerfilUsuario getPerfilSession(HttpContextBase httpContext) {
            var webSession = new WebCurrentSession(httpContext);
            return webSession.EsLogeado;
        }

        //set perfil session
        private static PerfilUsuario setPerfilSession(HttpContextBase httpContext, PerfilUsuario perfilUsuario)
        {
            var webSession = new WebCurrentSession(httpContext);
            webSession.EsLogeado = perfilUsuario;
            return webSession.EsLogeado;
        }

        //remove session
        private static WebCurrentSession removeSession(HttpContextBase httpContext)
        {
            var webSession = new WebCurrentSession(httpContext);
            webSession.EsLogeado = null;
            return null;
        }

        //get session
        public static WebCurrentSession getSession(HttpContextBase httpContext)
        {
            return new WebCurrentSession(httpContext);
        }

        public void getViewSessionPerfil(bool Remove = false)
        {
            if (Remove)
            {
                ViewBag.perfil = removeSession(this.HttpContext);
            }
            else
            {
                ViewBag.perfil = getPerfilSession(this.HttpContext);
            }
        }

        //recover or set state of transacction
        public string stateViewTransaction
        {
            get { return (string)(TempData["estado"]); }
            set { TempData["estado"] = value; }
        }

        public void getViewSession(PerfilUsuario perfilUsuario)
        {
            var webSession = getSession(this.HttpContext);
            webSession.EsLogeado = perfilUsuario;
            ViewBag.perfil = webSession.EsLogeado;
        }

        public string setLetterCaptcha()
        {
            var captcha = new Captcha();
            string letters = captcha.GetRandomCaptcha();

            var webSession = getSession(this.HttpContext);
            var access = new AccessCaptcha
            {
                value = letters
            };

            webSession.EsCaptcha = access;

            return letters;
        }

        public string getLetterCaptcha()
        {
            var webSession = getSession(this.HttpContext);
            return webSession.EsCaptcha.value;
        }

        public void getViewCaptcha(string texto)
        {
            string imgSrc = string.Empty;

            try
            {
                //allowed process generated captcha
                if(getAllowedCaptcha())
                {
                    var captcha = new Captcha();

                    var bmp = captcha.CreateBitmapWithColor(100, 60,
                        new ColorBitmapRgb { blue = 244, green = 169, red = 3 },
                        texto
                       );

                    var arr = captcha.ConvertBitmpaToArray(bmp);

                    imgSrc = "data:image/jpg;base64, " + Convert.ToBase64String(arr, 0, arr.Length);

                }

                ViewBag.captcha = imgSrc;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                ViewBag.captcha = imgSrc;
            }
        }

        public bool getAllowedCaptcha()
        {
            return Convert.ToBoolean(Util.getAllowedCaptcha());
        }

        public string getStringConnection()
        {
            return Convert.ToString(Util.getCadenaConexion());
        }

    }
}