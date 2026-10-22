using GestorTareas.Data;
using GestorTareas.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.Services
{
    public class TareaService : ITareaService
    {
        private readonly AppDbContext _context;
        public TareaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tarea>> ObtenerTodasAsync()
        {
            return await _context.Tareas.Include(t => t.Usuario).ToListAsync();
        }

        public async Task<List<Tarea>> ObtenerPorEstadoAsync(string estado)
        {
            return await _context.Tareas.Include(t => t.Usuario).Where(t => t.Estado == estado).ToListAsync();
        }

        public async Task<List<Usuario>> ObtenerUsuariosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<bool> CrearAsync(Tarea tarea)
        {
            _context.Tareas.Add(tarea);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ActualizarEstadoAsync(int id, string nuevoEstado)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null) return false;
            tarea.Estado = nuevoEstado;
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> EliminarAsync(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if(tarea == null) return false;
            _context.Tareas.Remove(tarea);
            return await _context.SaveChangesAsync() > 0;
        }

        
    }
}
