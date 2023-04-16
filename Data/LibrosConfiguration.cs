using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservasCore6.Models;

namespace ReservasCore6.Data
{
    public class LibrosConfiguration
    {
        // se realiza una carga de ejemplo para hoteles
        public LibrosConfiguration(EntityTypeBuilder<Libro> entityLibro,
            EntityTypeBuilder<Editorial> entityEditorial,
            EntityTypeBuilder<Autor> entityAutor,
            EntityTypeBuilder<AutoresLibros> entityAutoresLibros)
        {
            var editoriales = new List<Editorial>();
            var libros = new List<Libro>();
            var autores = new List<Autor>();
            var autoresLibros = new List<AutoresLibros>();
            var random = new Random();
            var contador = 1;

            for (var i = 1; i <= 5; i++)
            {
                editoriales.Add(new Editorial
                {
                    IdEditorial = i,
                    Nombre = $"Editorial {i}",
                    Sede = $"Sede {i}"
                });

                autores.Add(new Autor
                {
                    IdAutor = i,
                    Nombre = $"Nombre {i}",
                    Apellidos = $"Apellido {i}"
                });

                for (var j = 1; j <= 5; j++)
                {
                    libros.Add(new Libro
                    {
                        IdLibro = contador,
                        IdEditorial = i,
                        Titulo = $"titulo libro {j}",
                        Sipnosis = $"sipnosis libro {j}",
                        Npaginas = random.Next(5, 100)
                    });

                    autoresLibros.Add(new AutoresLibros
                    {
                        IdLibro = contador,
                        IdAutor = random.Next(1, 5)
                    });
                    contador++;
                }
            }

            entityEditorial.HasData(editoriales);
            entityAutor.HasData(autores);
            entityLibro.HasData(libros);
            entityAutoresLibros.HasData(autoresLibros);

        }
    }
}
