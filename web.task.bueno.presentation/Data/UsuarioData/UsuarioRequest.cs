using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.task.bueno.presentation.Data.UsuarioData
{
    public class UsuarioRequest
    {
        public string Nombre { get; set; }
        public string Correo { get; set; } 
        public string Clave { get; set; }

        public string Salt { get; set; }
    }
}
