using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservasCore6.Models;

namespace ReservasCore6.Data
{
    public class UsuarioConfiguration
    {
        // se realiza una carga de ejemplo para usuario
        public UsuarioConfiguration(EntityTypeBuilder<Usuario> entityBuilder)
        {
            var usuarios = new List<Usuario>();

            for (var i = 1; i <= 5; i++)
            {
                usuarios.Add(new Usuario
                {
                    IdUsuario = i,
                    Nombre = $"usuario {i}",
                    Apellidos = $"apellido {i}",
                    Email = $"{i}email@dominio.com ",
                    Direccion = $"Direccion {i}",
                    Password = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92"

                }); ;
             }
            // se especifica que es data inicial
            entityBuilder.HasData(usuarios);
        }
    }
}
