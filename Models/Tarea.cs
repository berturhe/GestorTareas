using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Models
{
    public class Tarea
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(100)]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Descripcion { get; set; }

        public DateTime FechaVencimiento { get; set; }

        [Required]
        public string Estado { get; set; } = "Pendiente"; // "Pendiente", "En Progreso", "Completada"

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
