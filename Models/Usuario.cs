using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ReservasCore6.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public string? Apellidos { get; set; }
        [Required]
        
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
