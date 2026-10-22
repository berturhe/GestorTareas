using GestorTareas.Models;

namespace GestorTareas.Services
{
    public interface ITareaService
    {
        Task<List<Tarea>> ObtenerTodasAsync();
        Task<List<Tarea>> ObtenerPorEstadoAsync(string estado);
        Task<List<Usuario>> ObtenerUsuariosAsync();
        Task<bool> CrearAsync(Tarea tarea);
        Task<bool> ActualizarEstadoAsync(int id, string nuevoEstado);
        Task<bool> EliminarAsync(int id);
    }
}
