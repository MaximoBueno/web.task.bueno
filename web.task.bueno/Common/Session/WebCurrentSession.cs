using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web.task.bueno.Models;

namespace web.task.bueno.Common.Session
{
    public sealed class WebCurrentSession
    {
        public HttpContextBase _session { set; get; }
        public WebCurrentSession() { }
        public WebCurrentSession(HttpContextBase session)
        {
            this._session = session;
        }

        public PerfilUsuario EsLogeado
        {
            get { return (PerfilUsuario)(this._session.Session["EsLogeado"]); }
            set { this._session.Session["EsLogeado"] = value; }
        }

        public String EsPrimeraVez
        {
            get { return (string)(this._session.Session["EsPrimeraVez"]); }
            set { this._session.Session["EsPrimeraVez"] = value; }
        }
    }
}