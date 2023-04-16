using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservasCore6.Models
{  
    public class AutoresLibros
    {
        [Key]
        [Column(Order = 0)]
        public int IdAutor { get; set; }

        [Key]
        [Column(Order = 1)]
        public int IdLibro { get; set; }

        [ForeignKey("IdAutor")]
        public Autor Autor { get; set; }

        [ForeignKey("IdLibro")]
        public Libro Libro { get; set; }
    }
}
