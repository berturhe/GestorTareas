using GestorTareas.Data;
using GestorTareas.Services;
using GestorTareas.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestorTareas.Pages
{
    public class EditarModel : PageModel
    {
        private readonly ITareaService _tareaService;
        public EditarModel(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        [BindProperty]
        public TareaFormViewModel TareaVM { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var tarea = await _tareaService.ObtenerPorIdAsync(id);

            if (tarea == null) {
                return RedirectToPage("/Index");
            }

            TareaVM = new TareaFormViewModel
            {
                Id = tarea.Id,
                Titulo = tarea.Titulo,
                Descripcion = tarea.Descripcion,
                FechaVencimiento = tarea.FechaVencimiento,
                Estado = tarea.Estado,
                UsuarioId = tarea.UsuarioId
            };

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

            var tareaExistente = await _tareaService.ObtenerPorIdAsync(TareaVM.Id);
            if(tareaExistente == null)
            {
                return RedirectToPage("/Index");
            }

            tareaExistente.Titulo = TareaVM.Titulo;
            tareaExistente.Descripcion = TareaVM.Descripcion;
            tareaExistente.FechaVencimiento = TareaVM.FechaVencimiento;
            tareaExistente.Estado = TareaVM.Estado;
            tareaExistente.UsuarioId = TareaVM.UsuarioId;

            await _tareaService.ActualizarAsync(tareaExistente);

            return RedirectToPage("/Index");
        }

        public async Task CargarUsuariosListaAsync()
        {
            var usuarios = await _tareaService.ObtenerUsuariosAsync();

            TareaVM.VisualizarUsuarios = usuarios.Select(u => new SelectListItem { 
                Value = u.Id.ToString(),
                Text = u.Nombre
            }).ToList();
        }
    }
}
