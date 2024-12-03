using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using web.task.bueno.domain;
using web.task.bueno.presentation.Repositories.Interfaces;

namespace web.task.bueno.presentation.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        public readonly ModelTestTask _context;

        public UsuarioRepository(string bd) { 
            _context = new ModelTestTask(bd);
        }

        public async Task<Usuario> CrearUsuario(Usuario usuario)
        {
            usuario.FechaCreacion = DateTime.Now;
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario> Login(string correo, string clave)
        {
            return await _context.Usuario.Where(x => x.Correo == correo && x.Clave == clave).FirstOrDefaultAsync();
        }

    }
}
