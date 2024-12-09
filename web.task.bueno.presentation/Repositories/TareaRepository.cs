using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.task.bueno.domain;
using web.task.bueno.presentation.Repositories.Interfaces;

namespace web.task.bueno.presentation.Repositories
{
    public class TareaRepository : ITareaRepository
    {
        public readonly ModelTestTask _context;

        public TareaRepository(string bd) { 
            _context = new ModelTestTask(bd);
        }

        public async Task<Tarea> agregarTarea(Tarea tarea)
        {
            tarea.FechaCreacion = DateTime.Now;
            _context.Tarea.Add(tarea);
            await _context.SaveChangesAsync();

            return tarea;
        }

        public async Task<bool> editarTarea(int idTarea, string titulo, string descripcion)
        {
            var tarea = await _context.Tarea.Where(x => x.Id == idTarea).FirstOrDefaultAsync();

            if (tarea == null) return await Task.FromResult(false);

            tarea.FechaModificacion = DateTime.Now;
            tarea.Titulo = titulo;
            tarea.Descripcion = descripcion;

            _context.Tarea.AddOrUpdate(tarea);
            await _context.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<bool> eliminarTarea(int idTarea)
        {
            var tarea = await _context.Tarea.Where(x => x.Id == idTarea).FirstOrDefaultAsync();

            if (tarea == null) return await Task.FromResult(false);

            _context.Tarea.Remove(tarea);
            await _context.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<List<Tarea>> ListarTarea()
        {
            return await _context.Tarea.ToListAsync();
        }

        public async Task<List<Tarea>> ListarTareaPorUsuario(int idUsuario)
        {
            return await _context.Tarea.Where(x => x.IdUsuario == idUsuario).ToListAsync();
        }

        public async Task<Tarea> obtenerTarea(int idTarea)
        {
            return await _context.Tarea.Where(x => x.Id == idTarea).FirstOrDefaultAsync();
        }
    }
}
