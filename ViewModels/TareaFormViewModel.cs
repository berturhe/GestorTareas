using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GestorTareas.ViewModels
{
    public class TareaFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El titulo de la tarea es obligatorio.")]
        [StringLength(200,ErrorMessage = "El título no puede superar los 200 caracteres.")]
        [Display(Name = "Título de la Tarea")]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "La descripción no puede superar los 1000 caracteres.")]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name ="Fecha de Vencimiento")]
        public DateTime FechaVencimiento { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "El estado inicial es obligatorio")]
        [Display(Name = "Estado")]
        public string Estado { get; set; } = "Pendiente";

        [Required(ErrorMessage = "Debe asignar un usuario responsable")]
        [Display(Name = "Responsable Asignado")]
        public int UsuarioId { get; set; }

        public List<SelectListItem> VisualizarUsuarios { get; set; } = new();

    }
}
