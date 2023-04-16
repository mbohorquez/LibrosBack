using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservasCore6.Models
{
    public class Libro
    {
        [Key]
        public int IdLibro { get; set; }
        [Required]
        public int? IdEditorial { get; set; }
        [Required]
        public string? Titulo { get; set; }
        [Required]
        public string? Sipnosis { get; set; }
        [Required]
        public int Npaginas { get; set; }

        [ForeignKey("IdEditorial")]
        public virtual Editorial? Editorial { get; set; }
        public ICollection<AutoresLibros> AutoresLibros { get; set; }

        public Libro()
        {
            AutoresLibros = new List<AutoresLibros>();
        }
    }

}
