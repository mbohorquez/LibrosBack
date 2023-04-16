using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReservasCore6.Data;
using ReservasCore6.Models;
using ReservasCore6.Models.Request;
using ReservasCore6.Models.Response;
using ReservasCore6.Models.Utils;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReservasCore6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LibrosController : ControllerBase
    {
        private readonly ILogger<LibrosController> _logger;
        private readonly AplicationDbContext _context;
        public LibrosController(ILogger<LibrosController> logger, AplicationDbContext contexto)
        {
            _logger = logger;
            _context = contexto;
        }
        /// <summary>
        /// Trae la data general de los libros
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetLibros([FromQuery] Pagination pagination)
        {
            var mensaje = "";
            Respuesta respuesta = new Respuesta();

            var query = _context.Libros.OrderBy(x => x.Titulo);

            // Aplicar paginación
            var skip = (pagination.PageNumber - 1) * pagination.PageSize;
            var libros = query.Skip(skip).Take(pagination.PageSize).ToList();

            // Calcular el número total de páginas
            var totalLibros = query.Count();
            var totalPages = (int)Math.Ceiling(totalLibros / (double)pagination.PageSize);

            var metadata = new
            {
                totalPages,
                totalLibros,
                pagination.PageNumber,
                pagination.PageSize
            };

            if (libros == null || libros.Count == 0)
            {
                mensaje = $"No se encontro data relacionada de libros";
                _logger.LogWarning(mensaje);
                respuesta.Mensaje = mensaje;
                return NotFound(respuesta);
            }

            mensaje = $"Existen libros";
            _logger.LogInformation(mensaje);

            // Incluir la metadata de paginación en la respuesta
            //Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(libros);
        }
        /// <summary>
        /// trae el detalle de un libro con su editorial y autores
        /// </summary>
        /// <param name="id">id del libro</param>
        /// <returns>json con el detalle del libro</returns>
        [HttpGet("{id}")]
        public  IActionResult GetDetalleLibro(int id)
        {
            var mensaje = "";
            Respuesta respuesta = new Respuesta();
            var librosAutoresEditorial = _context.Libros?.Include(x => x.Editorial)
                                                        .Include(x => x.AutoresLibros)
                                                        .ThenInclude(al => al.Autor)
                                                        .SingleOrDefault(x => x.IdLibro == id);
            // Esto se incluyo para prevenir la consulta ciclica y para que no aparesca el singno "$" en los id
            // seria oportuno refactorizar en el program.cs pero aun no encuentro la manera adecuada
            var settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            string json = JsonConvert.SerializeObject(librosAutoresEditorial, settings);

            if (librosAutoresEditorial == null)
            {
                mensaje = $"No se encontro data relacionada del detalle del libro";
                _logger.LogWarning(mensaje);
                respuesta.Mensaje = mensaje;
                return NotFound(respuesta);
            }
            mensaje = $"Existe detalle del libro";
            _logger.LogInformation(mensaje);
            return Ok(json);
        }

        [HttpPost]
        public IActionResult PostLibro([FromBody] LibroRequest crearLibroDto)
        {
            // Validar el objeto DTO
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Crear un nuevo objeto Libro
            var libro = new Libro
            {
                IdEditorial = crearLibroDto.IdEditorial,
                Titulo = crearLibroDto.Titulo,
                Sipnosis = crearLibroDto.Sipnosis,
                Npaginas = crearLibroDto.Npaginas
            };

            // Obtener la editorial correspondiente y asignarla al libro
            var editorial = _context.Editorial.FirstOrDefault(e => e.IdEditorial == crearLibroDto.IdEditorial);
            if (editorial == null)
            {
                return NotFound(new { Mensaje = $"No se encontró la editorial con Id {crearLibroDto.IdEditorial}" });
            }
            libro.Editorial = editorial;

            // Asignar los autores correspondientes al libro
            foreach (var idAutor in crearLibroDto.IdAutores)
            {
                var autor = _context.Autor.FirstOrDefault(a => a.IdAutor == idAutor);
                if (autor == null)
                {
                    return NotFound(new { Mensaje = $"No se encontró el autor con Id {idAutor}" });
                }
                libro.AutoresLibros.Add(new AutoresLibros { Autor = autor, Libro = libro });
            }

            // Guardar el nuevo libro en la base de datos
            _context.Libros.Add(libro);
            _context.SaveChanges();

            return CreatedAtAction(nameof(PostLibro), new { id = libro.IdLibro });
        }

        [HttpPut("{id}")]
        public IActionResult PutLibro(int id, [FromBody] LibroRequest actualizarLibroDto)
        {
            // Validar el objeto DTO
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Obtener el libro correspondiente y actualizarlo
            var libro = _context.Libros
                .Include(l => l.Editorial)
                .Include(l => l.AutoresLibros)
                    .ThenInclude(al => al.Autor)
                .FirstOrDefault(l => l.IdLibro == id);
            if (libro == null)
            {
                return NotFound(new { Mensaje = $"No se encontró el libro con Id {id}" });
            }
            libro.IdEditorial = actualizarLibroDto.IdEditorial;
            libro.Titulo = actualizarLibroDto.Titulo;
            libro.Sipnosis = actualizarLibroDto.Sipnosis;
            libro.Npaginas = actualizarLibroDto.Npaginas;

            // Actualizar la editorial correspondiente del libro
            var editorial = _context.Editorial.FirstOrDefault(e => e.IdEditorial == actualizarLibroDto.IdEditorial);
            if (editorial == null)
            {
                return NotFound(new { Mensaje = $"No se encontró la editorial con Id {actualizarLibroDto.IdEditorial}" });
            }
            libro.Editorial = editorial;

            // Actualizar los autores correspondientes del libro
            libro.AutoresLibros.Clear();
            foreach (var idAutor in actualizarLibroDto.IdAutores)
            {
                var autor = _context.Autor.FirstOrDefault(a => a.IdAutor == idAutor);
                if (autor == null)
                {
                    return NotFound(new { Mensaje = $"No se encontró el autor con Id {idAutor}" });
                }
                libro.AutoresLibros.Add(new AutoresLibros { Autor = autor, Libro = libro });
            }

            // Guardar los cambios en la base de datos
            _context.Libros.Update(libro);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletedLibro(int id)
        {
            var libro = _context.Libros.FirstOrDefault(l => l.IdLibro == id);
            if (libro == null)
            {
                return NotFound(new { Mensaje = $"No se encontró el libro con Id {id}" });
            }

            _context.Libros.Remove(libro);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
