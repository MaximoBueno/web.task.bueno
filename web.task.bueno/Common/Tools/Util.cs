using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.task.bueno.Models;

namespace web.task.bueno.Tools
{
    public static class Util
    {

        public static string getCadenaConexion()
        {
            return ConfigurationManager.ConnectionStrings["ModelTestTask"].ConnectionString;
        }

        public static string getAllowedCaptcha()
        {
            return ConfigurationManager.AppSettings["AllowedCaptcha"];
        }

        //evaluar
        public static int getTimeOut() {
            try
            {
                return Convert.ToInt32((
                    (System.Web.Configuration.SessionStateSection)ConfigurationManager.GetSection("system.web/sessionState")
                ).Timeout.TotalMinutes);
            }
            catch
            {
                return -1;
            }
        }
    }
}