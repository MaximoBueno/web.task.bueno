using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.task.bueno.domain;

namespace web.task.bueno.presentation.Repositories.Interfaces
{
    public interface ITareaRepository
    {
        Task<List<Tarea>> ListarTarea();
        Task<List<Tarea>> ListarTareaPorUsuario(int idUsuario);
        Task<Tarea> obtenerTarea(int idTarea);
        Task<Tarea> agregarTarea(Tarea tarea);
        Task<bool> editarTarea(int idTarea, string titulo, string descripcion);
        Task<bool> eliminarTarea(int idTarea);

    }
}
