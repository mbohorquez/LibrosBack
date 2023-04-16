using System.ComponentModel.DataAnnotations;

namespace ReservasCore6.Models
{  
    public class Autor
    {
        [Key]
        public int IdAutor { get; set; }
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public string? Apellidos { get; set; }
        public ICollection<AutoresLibros>? AutoresLibros { get; set; }
    }
}
