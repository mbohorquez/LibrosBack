using System.ComponentModel.DataAnnotations;

namespace ReservasCore6.Models.Request
{
    public class LibroRequest
    {
        [Required]
        public int IdEditorial { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Sipnosis { get; set; }
        [Required]
        public int Npaginas { get; set; }
        [Required]
        public List<int> IdAutores { get; set; }
    }
}
