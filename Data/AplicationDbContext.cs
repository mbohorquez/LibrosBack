using Microsoft.EntityFrameworkCore;
using ReservasCore6.Models;
using ReservasCore6.Services;
using System.Reflection.Emit;

namespace ReservasCore6.Data
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) { }

        //crearDbset - se usa para traer la data de BD y mapearla
        public DbSet<Editorial> Editorial { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Autor> Autor { get; set; }
        public DbSet<AutoresLibros> AutoresLibros { get; set; }

        // se trae la linea de conexion de bd
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // se realiza opcion, si no esta configurada la migracion entrara a crear una, si no el NUnit creara una Bd temporal y en memoria 
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

                var connectionString = configuration.GetConnectionString("Conexion");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // email unico en BD
            builder.Entity<Usuario>()
            .HasIndex(x => x.Email)
            .IsUnique(true);

            builder.Entity<AutoresLibros>()
            .HasKey(al => new { al.IdAutor, al.IdLibro });

            // se realiza una carga inicial
            base.OnModelCreating(builder);
            ModelConfig(builder);
        }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            // se traen las configuraciones y data de ejemplo
            new LibrosConfiguration(modelBuilder.Entity<Libro>(), modelBuilder.Entity<Editorial>(),
                                    modelBuilder.Entity<Autor>(), modelBuilder.Entity<AutoresLibros>());
            new UsuarioConfiguration(modelBuilder.Entity<Usuario>());
        }


    }
}
