using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.task.bueno.domain;
using web.task.bueno.presentation.Data.UsuarioData;

namespace web.task.bueno.presentation.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> Login(string correo);

        Task<Usuario> CrearUsuario(UsuarioRequest usuario);

    }
}
