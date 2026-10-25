using GestorTareas.Models;
using GestorTareas.Services;
using GestorTareas.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestorTareas.Pages
{
    public class CrearModel : PageModel
    {
        private readonly ITareaService _tareaService;

        public CrearModel(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        [BindProperty]
        public TareaFormViewModel TareaVM { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            await CargarUsuariosListaAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await CargarUsuariosListaAsync();
                return Page();
            }

            var nuevaTarea = new Tarea
            {
                Titulo = TareaVM.Titulo,
                Descripcion = TareaVM.Descripcion,
                FechaVencimiento = TareaVM.FechaVencimiento,
                Estado = "Pendiente",
                UsuarioId = TareaVM.UsuarioId,
            };

            await _tareaService.CrearAsync(nuevaTarea);

            return RedirectToPage("/Index");
        }
        private async Task CargarUsuariosListaAsync()
        {
            var usuarios = await _tareaService.ObtenerUsuariosAsync();
            TareaVM.VisualizarUsuarios = usuarios.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Nombre
            }).ToList();
        }
    }
}
