using GestorTareas.Models;
using GestorTareas.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestorTareas.Pages;

public class IndexModel : PageModel
{
    private readonly ITareaService _tareaService;

    public IndexModel(ITareaService tareaService)
    {
       _tareaService = tareaService;
    }

    public Dictionary<string, List<Tarea>> TareasPorEstado { get; set; } = new();
    public List<string> EstadosDisponibles { get; set; } = new(){ "Pendiente", "En Progreso", "Completada" };

    public async Task OnGetAsync()
    {
        var tareas = await _tareaService.ObtenerTodasAsync();

        TareasPorEstado = tareas.GroupBy(t=>t.Estado)
            .ToDictionary(g=>g.Key,g=>g.ToList());
    }

    public async Task<IActionResult> OnPostEliminarAsync(int id)
    {
        await _tareaService.EliminarAsync(id);
        return RedirectToPage("/Index");
    }

    public async Task<IActionResult> OnPostCambiarEstadoAsync(int id,string nuevoEstado)
    {
        var exito = await _tareaService.ActualizarEstadoAsync(id, nuevoEstado);

        return new JsonResult(new
        {
            success = exito,
            id = id,
            nuevoEstado = nuevoEstado
        });
    }

}
