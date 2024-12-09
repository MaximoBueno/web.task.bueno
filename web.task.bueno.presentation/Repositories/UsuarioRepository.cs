using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using web.task.bueno.domain;
using web.task.bueno.presentation.Data.UsuarioData;
using web.task.bueno.presentation.Repositories.Interfaces;

namespace web.task.bueno.presentation.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        public readonly ModelTestTask _context;

        public UsuarioRepository(string bd) { 
            _context = new ModelTestTask(bd);
        }

        public async Task<Usuario> CrearUsuario(UsuarioRequest usuario)
        {
            //Transfer data
            var usuarioNew = new Usuario();
            usuarioNew.NombreCompleto = usuario.Nombre;
            usuarioNew.Correo = usuario.Correo;
            usuarioNew.Clave = usuario.Clave;
            usuarioNew.FechaCreacion = DateTime.Now;
            //Transfer data
            _context.Usuario.Add(usuarioNew);
            await _context.SaveChangesAsync();

            return usuarioNew;
        }

        public async Task<Usuario> Login(string correo)
        {
            return await _context.Usuario.Where(x => x.Correo == correo).FirstOrDefaultAsync();
        }

    }
}
