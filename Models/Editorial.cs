using System.ComponentModel.DataAnnotations;

namespace ReservasCore6.Models
{  
    public class Editorial
    {
        [Key]
        public int IdEditorial { get; set; }
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public string? Sede { get; set; }
        
    }
}
