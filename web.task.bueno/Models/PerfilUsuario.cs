using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.task.bueno.Models
{
    [Serializable]
    public class PerfilUsuario
    {
        public int id { get; set; }
        public string nombreCompleto { get; set; }
        public string correo { get; set; }
    }

    [Serializable]
    public class AccessCaptcha
    {
        public string value {  get; set; }
    }
}