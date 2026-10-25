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

    public List<Tarea> Tareas { get; set; } = new();

    public async Task OnGetAsync()
    {
        Tareas = await _tareaService.ObtenerTodasAsync();
    }

    public async Task<IActionResult> OnPostEliminarAsync(int id)
    {
        await _tareaService.EliminarAsync(id);
        return RedirectToPage("/Index");
    }

}
