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

    public async Task OnGetAsync()
    {
        var tareas = await _tareaService.ObtenerTodasAsync();

        var ordenColumnas = new List<string> { "Pendiente", "En Progreso", "Completada" };

        TareasPorEstado = tareas.GroupBy(t=>t.Estado)
            .OrderBy(g => ordenColumnas.IndexOf(g.Key))
            .ToDictionary(g=>g.Key,g=>g.ToList());
    }

    public async Task<IActionResult> OnPostEliminarAsync(int id)
    {
        await _tareaService.EliminarAsync(id);
        return RedirectToPage("/Index");
    }

}
