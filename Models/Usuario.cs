using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email {  get; set; } = string.Empty;

        public List<Tarea> Tareas { get; set; } = new List<Tarea>();
    }
}
