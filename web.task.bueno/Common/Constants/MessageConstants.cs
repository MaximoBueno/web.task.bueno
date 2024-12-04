using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.task.bueno.Common.Constants
{
    public static class MessageConstants
    {
        public static string ERROR_GENERICO = "Ha ocurrido un error, comunicarse con el administrador del sistema.";
        public static string ERROR_GENERICO_REGISTRO = "Error al registrar.";
        public static string ERROR_CAPTCHA = "El Captcha ingresado es incorrecto.";
        public static string ERROR_LOGIN= "El Correo o Clave son incorrectos";
        public static string EXITO_USUARIO_REGISTRO = "Se ha registrado el usuario correctamente. Favor de Iniciar Sesión.";
    }
}