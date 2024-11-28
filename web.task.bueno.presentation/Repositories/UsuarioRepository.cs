using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
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

        public async Task<Usuario> Login(string correo, string clave)
        {
            return await _context.Usuario.Where(x => x.Correo == correo && x.Clave == clave).FirstOrDefaultAsync();
        }
    }
}
