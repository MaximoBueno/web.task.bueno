using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.task.bueno.Common.Session;
using web.task.bueno.Models;

namespace web.task.bueno.Common.Base
{
    public abstract class BaseCustomController : Controller
    {
        //get perfil sesion
        private static PerfilUsuario getPerfilSession(HttpContextBase httpContext) {
            var webSession = new WebCurrentSession(httpContext);
            return webSession.EsLogeado;
        }

        //set perfil sesion
        private static PerfilUsuario setPerfilSession(HttpContextBase httpContext,PerfilUsuario perfilUsuario)
        {
            var webSession = new WebCurrentSession(httpContext);
            webSession.EsLogeado = perfilUsuario;
            return webSession.EsLogeado;
        }

        //remove sesion
        private static WebCurrentSession removeSession(HttpContextBase httpContext)
        {
            var webSession = new WebCurrentSession(httpContext);
            webSession.EsPrimeraVez = "";
            webSession.EsLogeado = null;

            return null;
        }

        //get sesion
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

        public void getViewSession(PerfilUsuario perfilUsuario)
        {
            var webSession = getSession(this.HttpContext);
            webSession.EsLogeado = perfilUsuario;
            ViewBag.perfil = webSession.EsLogeado;
        }

    }
}